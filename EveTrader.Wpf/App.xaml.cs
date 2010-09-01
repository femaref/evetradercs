﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Reflection;
using EveTrader.Core.Controllers;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Waf.Applications;
using System.Windows.Threading;
using System.Diagnostics;
using System.Waf;
using EveTrader.Core.DataConverter;
using System.IO;
using EveTrader.Core.Model;
using System.Data.SQLite;
using System.Data.EntityClient;
using System.EnterpriseServices.Internal;
using EveTrader.Core.Services;

namespace EveTrader.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ApplicationController controller;

        static App()
        {
#if (DEBUG)
            WafConfiguration.Debug = true;
#endif
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            
#if (DEBUG != true)
            // Don't handle the exceptions in Debug mode because otherwise the Debugger wouldn't
            // jump into the code when an exception occurs.
            DispatcherUnhandledException += AppDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
#endif
            DirectoryInfo appData = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EveTrader"));
            FileInfo databaseInfo = new FileInfo(Path.Combine(appData.FullName, "EveTrader.db"));
            FileInfo settingsInfo = new FileInfo(Path.Combine(appData.FullName, "settings.xml"));
            FileInfo staticDatabase = new FileInfo(Path.Combine(appData.FullName, "static.db"));
            FileInfo staticDatabaseRessource = new FileInfo("static.db");

            if (!appData.Exists)
                appData.Create();

            if (settingsInfo.Exists && settingsInfo.Length > 0)
            {
                string fileTime = DateTime.Now.ToFileTime().ToString();
                string backupPath = string.Format("backup_{0}", fileTime);
                if (!Directory.Exists(Path.Combine(appData.FullName, backupPath)))
                    appData.CreateSubdirectory(backupPath);

                File.Copy(settingsInfo.FullName, Path.Combine(appData.FullName, backupPath, "settings.xml"));


                //refactor to a more detailed window with progressbar etc
                var result = MessageBox.Show("settings.xml found. Do you want to convert the data? This can take some time", "Convert data", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    XmlToSqlite xts = new XmlToSqlite(settingsInfo.FullName);
                    xts.Convert();
                }

                settingsInfo.Delete();
            }

            //if no database exists, create a fresh one
            if (!databaseInfo.Exists || databaseInfo.Length == 0)
                TraderModel.CreateDatabase(databaseInfo.FullName);

            //copy over static.db
            if (!staticDatabase.Exists)
                File.Copy(staticDatabaseRessource.FullName, staticDatabase.FullName);


            EntityConnectionStringBuilder traderModelEntityBuilder = CreateConnectionBuilder(databaseInfo.FullName, @"res://*/Model.TraderModel.csdl|res://*/Model.TraderModel.ssdl|res://*/Model.TraderModel.msl");

            //Prune incomplete entities, wrongly created transactions etc
            using (TraderModel tm = new TraderModel(traderModelEntityBuilder))
                tm.Prune();

            EntityConnectionStringBuilder staticModelEntityBuilder = CreateConnectionBuilder(staticDatabase.FullName, @"res://*/Model.StaticModel.csdl|res://*/Model.StaticModel.ssdl|res://*/Model.StaticModel.msl");

            base.OnStartup(e);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            AggregateCatalog catalog = new AggregateCatalog();
            // Add the WpfApplicationFramework assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Controller).Assembly));
            // Add Frontend Assembly
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            // Add EveTrader.Core
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ApplicationController).Assembly));

            CompositionContainer container = new CompositionContainer(catalog);
            CompositionBatch batch = new CompositionBatch();


            batch.AddExportedValue(container);

            //add connections to the batch
            batch.AddExportedValue("TraderModelConnection", traderModelEntityBuilder);
            batch.AddExportedValue("StaticModelConnection", staticModelEntityBuilder);

            container.Compose(batch);

           // var updater = container.GetExportedValue<IApplicationUpdateService>();
           // updater.CheckUpdate();
            
            controller = container.GetExportedValue<ApplicationController>();

            sw.Stop();
            Debug.WriteLine(sw.Elapsed.TotalSeconds);

            controller.Run();
        }

        private EntityConnectionStringBuilder CreateConnectionBuilder(string source, string metadata)
        {
            SQLiteConnectionStringBuilder sqliteBuilder = new SQLiteConnectionStringBuilder();
            sqliteBuilder.DataSource = source;

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Provider = "System.Data.SQLite";
            entityBuilder.ProviderConnectionString = sqliteBuilder.ToString();
            entityBuilder.Metadata = metadata;

            return entityBuilder;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            controller.Shutdown();
            base.OnExit(e);
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception, false);
            e.Handled = true;
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception, e.IsTerminating);
        }

        private static void HandleException(Exception e, bool isTerminating)
        {
            if (e == null) { return; }

            Trace.TraceError(e.ToString());

            DirectoryInfo myDoc = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EveTrader"));

            if (!myDoc.Exists)
                myDoc.Create();

            string filePath = Path.Combine(myDoc.FullName, string.Format("{0}.log", DateTime.UtcNow.ToFileTimeUtc()));
            File.WriteAllText(filePath, e.ToString());

            if (!isTerminating)
            {
                
            }
        }
    }
}