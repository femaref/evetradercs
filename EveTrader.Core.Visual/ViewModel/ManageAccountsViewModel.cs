﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Waf.Applications;
using EveTrader.Core.Visual.View;
using EveTrader.Core.Model.Trader;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using EveTrader.Core.Network.Requests.CCP;
using EveTrader.Core.Collections.ObjectModel;
using EveTrader.Core.Visual.ViewModel.Display;
using MoreLinq;
using EveTrader.Core.Controllers;
using EveTrader.Core.Updater.CCP;
using EveTrader.Core.Services;
using System.Windows.Input;
using System.Threading;
using System.Xml.Linq;
using System.IO;
using EveTrader.Core.ViewModel;

namespace EveTrader.Core.Visual.ViewModel
{
    [Export]
    public class ManageAccountsViewModel : ViewModel<IManageAccountsView>, IRefreshableViewModel, ISettingsPage
    {
        private readonly TraderModel iModel;
        private readonly IUpdateService iUpdater;
        private readonly EntityFactory iFactory;
        private readonly IDatabaseExportService iExport;

        private DelegateCommand iRequestDataCommand;
        private DelegateCommand iAbortRequestCommand;
        private DelegateCommand iAddCharactersCommand;
        private DelegateCommand iUpdateAccountCommand;
        private DelegateCommand iDeleteCharacterCommand;

        private object iUpdaterLock = new object();

        public ICommand RequestDataCommand
        {
            get { return iRequestDataCommand; }
        }
        public ICommand AbortRequestCommand
        {
            get { return iAbortRequestCommand; }
        }
        public ICommand AddCharactersCommand
        {
            get { return iAddCharactersCommand; }
        }
        public ICommand UpdateAccountCommand
        {
            get { return iUpdateAccountCommand; }
        }
        public ICommand DeleteCharacterCommand
        {
            get { return iDeleteCharacterCommand; }
        }

        private long iCurrentUserID;

        public long CurrentUserID
        {
            get { return iCurrentUserID; }
            set
            {
                lock (iUpdaterLock)
                {
                    iCurrentUserID = value;
                    RaisePropertyChanged("CurrentUserID");
                }
            }
        }

        private string iCurrentApiKey;

        public string CurrentApiKey
        {
            get { return iCurrentApiKey; }
            set
            {
                lock (iUpdaterLock)
                {
                    iCurrentApiKey = value;
                    RaisePropertyChanged("CurrentApiKey");
                }
            }
        }

        public Characters CurrentItem
        {
            get { return iCurrentItem; }
            set
            {
                this.iCurrentItem = value;
                RaisePropertyChanged("CurrentItem");
            }
        }



        private bool iDataRequestable = false;
        private bool iDataPresent = false;

        public SmartObservableCollection<Characters> CurrentCharacters { get; private set; }
        public SmartObservableCollection<Selectable<Characters>> RequestedCharacters { get; private set; }
        public bool DataPresent
        {
            get
            {
                return iDataPresent;
            }
            private set
            {
                iDataPresent = value;
                RaisePropertyChanged("DataPresent");
            }
        }
        public bool DataRequestable
        {
            get
            {
                return iDataRequestable;
            }
            private set
            {
                iDataRequestable = value;
                RaisePropertyChanged("DataRequestable");
            }
        }

        [ImportingConstructor]
        public ManageAccountsViewModel(
            IManageAccountsView view, 
            [Import(RequiredCreationPolicy = CreationPolicy.Shared)] TraderModel tm, 
            IUpdateService us, 
            EntityFactory ef,
            IDatabaseExportService export)
            : base(view)
        {
            iModel = tm;
            iUpdater = us;
            iFactory = ef;
            iExport = export;

            CurrentCharacters = new SmartObservableCollection<Characters>(view.Invoke);
            RequestedCharacters = new SmartObservableCollection<Selectable<Characters>>(view.Invoke);
            DataRequestable = true;
          //  view.Closing += new System.ComponentModel.CancelEventHandler(ViewCore_Closing);

            iRequestDataCommand = new DelegateCommand(RequestData, () => DataRequestable);
            iAbortRequestCommand = new DelegateCommand(AbortRequest);
            iAddCharactersCommand = new DelegateCommand(AddCharacters, () => DataPresent);
            iUpdateAccountCommand = new DelegateCommand(
                () => 
                { 
                    if (CurrentItem != null) 
                        CurrentUserID = CurrentItem.Account.ID; 
                });
            iDeleteCharacterCommand = new DelegateCommand(
                () =>
                {
                    if (CurrentItem != null)
                        DeleteCharacter(CurrentItem);
                });

            iUpdater.UpdateStarted += (sender, e) =>
                {
                    Updating = true;
                };

            iUpdater.UpdateCompleted += (sender, e) =>
                {
                    Updating = false;
                };

            iExport.Started += (sender, e) =>
                {
                    Updating = true;
                };
            iExport.Completed += (sender, e) =>
                {
                    Updating = false;
                };

            this.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "DataRequestable")
                        this.ViewCore.BeginInvoke(() => iRequestDataCommand.RaiseCanExecuteChanged());
                    if (e.PropertyName == "DataPresent")
                        this.ViewCore.BeginInvoke(() => iAddCharactersCommand.RaiseCanExecuteChanged());
                };

            Refresh();
        }

        private void DeleteCharacter(Characters c)
        {

            ThreadStart ts = new ThreadStart(() => ThreadedDelete(c));
            Thread export = new Thread(ts);
            export.Name = "Exporting Thread";
            export.Start();
        }

        //hacky code
        private void ThreadedDelete(Characters c)
        {
            lock (iUpdaterLock)
            {
                Updating = true;

                XDocument xd = iExport.Export(c.Account);

                DirectoryInfo di = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EveTrader"));
                FileInfo fi = new FileInfo(string.Format("{0}_{1}.xml", c.Account.ID, DateTime.UtcNow.ToFileTimeUtc()));

                File.WriteAllText(Path.Combine(di.FullName, fi.Name), xd.ToString());

                iModel.Entity.DeleteObject(c);
                iModel.SaveChanges();
                Updating = false;
            }
        }

        public void Show()
        {
           // this.ViewCore.Show();
        }
        public void Shutdown()
        {
            //this.ViewCore.Close();
        }
        public void Refresh()
        {
            CurrentCharacters.Clear();
            iModel.Entity.OfType<Characters>().ToList().ForEach(x => CurrentCharacters.Add(x));
        }

        private void RequestData()
        {
            ThreadStart ts = new ThreadStart(ThreadedRequestData);
            Thread t = new Thread(ts);
            t.Name = "Request Thread";
            t.Start();
        }
        private void ThreadedRequestData()
        {
            lock (iUpdaterLock)
            {
                Updating = true;

                var account = iModel.Accounts.Where(a => a.ID == CurrentUserID).FirstOrDefault();
                if (account == null)
                {
                    account = new Accounts() { ID = CurrentUserID, ApiKey = CurrentApiKey };
                    iModel.Accounts.AddObject(account);
                }
                if (account.ApiKey != CurrentApiKey)
                    account.ApiKey = CurrentApiKey;
                iModel.SaveChanges();

                CharacterListRequest clr = new CharacterListRequest(new Accounts() { ID = account.ID, ApiKey = account.ApiKey }, iModel.StillCached, iModel.SaveCache, iModel.LoadCache);
                var requestedCharacters = clr.Request().Select(c => new Selectable<Characters>(c, false));

                RequestedCharacters.AddRange(requestedCharacters.Where(s => !account.Entities.Any(e => e.ID == s.Item.ID)));

                DataRequestable = false;
                DataPresent = true;
                Updating = false;
            }
        }
        private void AbortRequest()
        {
            lock (iUpdaterLock)
            {
                Updating = true;

                RequestedCharacters.Clear();
                DataRequestable = true;
                DataPresent = false;

                Updating = false;
            }
        }
        private void AddCharacters()
        {
            ThreadStart ts = new ThreadStart(ThreadedAddCharacters);
            Thread t = new Thread(ts);
            t.Name = "Add characters Thread";
            t.Start();
        }

        private void ThreadedAddCharacters()
        {
            lock (iUpdaterLock)
            {
                Updating = true;

                long id = RequestedCharacters.First().Item.Account.ID;
                Accounts account = iModel.Accounts.First(a => a.ID == id);
                foreach (Characters c in RequestedCharacters.Where(s => s.IsSelected).Where(s => !iModel.Entity.Any(e => e.ID == s.Item.ID)))
                {
                    Characters cache = iFactory.CreateCharacter(c.ID, account);
                    cache.Corporation = iFactory.CreateCorporation(c.Corporation.ID, account, c.ID);
                    iModel.SaveChanges();

                    iUpdater.Update(cache);
                    iUpdater.Update(cache.Corporation);
                }
                RequestedCharacters.Clear();
                DataRequestable = true;
                DataPresent = false;
                Updating = false;
                Refresh();
            }
        }

        #region IRefreshableViewModel Members


        public void DataIncoming(object sender, EntitiesUpdatedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private bool iUpdating = false;
private  Characters iCurrentItem;

        public bool Updating
        {
            get { return iUpdating; }
            private set
            {
                iUpdating = value;
                RaisePropertyChanged("Updating");
            }
        }

        #endregion

        #region ISettingsPage Members
        public event EventHandler Closed;

        private void RaiseClosed()
        {
            var handler = Closed;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public string Name
        {
            get { return "Manage Accounts"; }
        }

        public int Index
        {
            get { return 0; }
        }
        #endregion
    }
}
