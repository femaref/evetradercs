﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Waf.Applications;
using System.Windows.Input;
using EveTrader.Core.Model.Trader;
using EveTrader.Core.Updater.CCP;
using EveTrader.Core.Visual.View;
using EveTrader.Core.Visual.ViewModel;
using EveTrader.Core.Model.Static;
using EveTrader.Core.Model;

namespace EveTrader.Core.Controllers
{
    [Export]
    public class ApplicationController : Controller
    {
        private readonly MainWindowViewModel iMainWindowViewModel;
        private readonly TraderModel iModel;
        private readonly CompositionContainer iContainer;
        private readonly DashboardController iDashboardController;
        private readonly WalletsController iWalletsController;
        private readonly MarketOrdersController iMarketOrdersController;
        private readonly ApplicationLogController iApplicationLogController;
        private readonly PriceCacheController iPriceCacheController;
        private readonly ReportController iReportController;
        private readonly JournalController iJournalController;
        private readonly TransactionsController iTransactionsController;

        private readonly ISettingsProvider iSettings;

        private readonly CharacterUpdater iCharacterUpdater;
        private readonly CorporationUpdater iCorporationUpdater;

        private readonly StaticModel iStaticData;

        private readonly List<Controller> iControllers = new List<Controller>();
        private ConverterController converterController;
        private SettingsController settingsController;
       
        [ImportingConstructor]
        public ApplicationController(
            MainWindowViewModel mainView,
            TraderModel tm,
            StaticModel sm,
            CompositionContainer container,
            DashboardController dashboardController,
            WalletsController walletsController,
            MarketOrdersController marketOrdersController,
            ApplicationLogController applicationLogController,
            PriceCacheController priceCacheController,
            JournalController journalController,
            TransactionsController transactionsController,
            ReportController reportController,
            ConverterController converterController,
            SettingsController settingsController,
            ISettingsProvider settingsProvider,
            CharacterUpdater charUpdater,
            CorporationUpdater corpUpdater)
        {
            iMainWindowViewModel = mainView;

            iModel = tm;
            iStaticData = sm;
            iContainer = container;
            iDashboardController = dashboardController;
            iWalletsController = walletsController;
            iMarketOrdersController = marketOrdersController;
            iApplicationLogController = applicationLogController;
            iPriceCacheController = priceCacheController;
            iJournalController = journalController;
            iTransactionsController = transactionsController;
            iReportController = reportController;
            this.converterController = converterController;
            this.settingsController = settingsController;

            iSettings = settingsProvider;

            iCharacterUpdater = charUpdater;
            iCorporationUpdater = corpUpdater;
        }

        public void Run()
        {
            iMainWindowViewModel.Show();
        }

        public void Shutdown()
        {
            iModel.Dispose();
        }
    }
}
