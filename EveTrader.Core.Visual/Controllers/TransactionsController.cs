﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Waf.Applications;
using EveTrader.Core.Visual.ViewModel;
using System.ComponentModel.Composition;
using EveTrader.Core.Services;

namespace EveTrader.Core.Controllers
{
    [Export]
    public class TransactionsController : Controller
    {
        private readonly MainWindowViewModel iMainView;
        private readonly TransactionsViewModel iTransactionsView;

        [ImportingConstructor]
        public TransactionsController(MainWindowViewModel mainView, TransactionsViewModel transactionsView, IUpdateService updater)
        {
            iMainView = mainView;
            iTransactionsView = transactionsView;

            iMainView.TransactionsView = iTransactionsView.View;

            updater.UpdateCompleted += iTransactionsView.DataIncoming;
        }
    }
}
