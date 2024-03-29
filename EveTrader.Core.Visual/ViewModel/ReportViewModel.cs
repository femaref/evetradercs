﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Waf.Applications;
using System.Windows.Input;
using EveTrader.Core.Collections.ObjectModel;
using EveTrader.Core.Model;
using EveTrader.Core.Model.Trader;
using EveTrader.Core.Services;
using EveTrader.Core.ViewModel;
using EveTrader.Core.Visual.View;
using EveTrader.Core.Visual.ViewModel.Display;

namespace EveTrader.Core.Visual.ViewModel
{
    [Export]
    public class ReportViewModel : ViewModel<IReportView>, IRefreshableViewModel
    {
        private readonly TraderModel iModel;
        private readonly ISettingsProvider iSettings;
        
        private object iUpdaterLock = new object();

        private string iConcatedEntities = "";
        private bool iUpdating = false;


        public SmartObservableCollection<Selectable<Entities>> Entities { get; private set; }

        public SmartObservableCollection<IReportPage> Reports { get; private set; }

        public DateTime StartDate
        {
            get
            {
                return iSettings.ReportStartDate;
            }
            set
            {
                iSettings.ReportStartDate = value;
                RaisePropertyChanged("StartDate");
            }
        }
        public DateTime EndDate
        {
            get
            {
                return iSettings.ReportEndDate;
            }
            set
            {
                iSettings.ReportEndDate = value;
                RaisePropertyChanged("EndDate");
            }
        }
        public bool ApplyStartFilter
        {
            get
            {
                return iSettings.ReportApplyStartFilter;
            }
            set
            {
                iSettings.ReportApplyStartFilter = value;
                RaisePropertyChanged("ApplyStartFilter");
                
            }
        }
        public bool ApplyEndFilter
        {
            get
            {
                return iSettings.ReportApplyEndFilter;
            }
            set
            {
                iSettings.ReportApplyEndFilter = value;
                RaisePropertyChanged("ApplyEndFilter");
                
            }
        }
        public bool Updating
        {
            get
            {
                return iUpdating;
            }
            set
            {
                iUpdating = value;
                RaisePropertyChanged("Updating");
            }
        }
        public string ConcatedEntities
        {
            get
            {
                return iConcatedEntities;
            }
        }
        public ICommand RefreshCommand { get; private set; }

        private bool test = false;

        [ImportingConstructor]
        public ReportViewModel(IReportView view, [Import(RequiredCreationPolicy = CreationPolicy.NonShared)] TraderModel tm, ISettingsProvider settings, IPriceSourceSelector selector, [ImportMany] IEnumerable<IReportPage> reports)
            : base(view)
        {
            iModel = tm;
            iSettings = settings;
            


            Entities = new SmartObservableCollection<Selectable<Entities>>(ViewCore.BeginInvoke);

            Reports = new SmartObservableCollection<IReportPage>(view.BeginInvoke);

            Reports.AddRange(reports.OrderBy(r => r.Index));

            foreach (var r in Reports)
            {
                this.ChildRefresh += r.Refresh;
                this.ChildCancel += r.Cancel;                
            }

            RefreshCommand = new DelegateCommand(() => 
                {
                    if (!test)
                    {
                        test = true;
                        Refresh();
                    }
                    else
                    {
                        test = false;
                        RaiseChildCancel();
                    }
                });

            RefreshEntities();
            
        }

        public void Refresh()
        {
            Thread updaterThread = new Thread(new ThreadStart(this.ThreadedRefresh));
            updaterThread.Name = "ReportRefresh";
            updaterThread.Start();
        }
        private void ThreadedRefresh()
        {
            lock(iUpdaterLock)
                RaiseChildRefresh();

            //lock (iUpdaterLock)
            //{
            //    this.Updating = true;

            //    iModel.Connection.Open();

            //    StationReport.Clear();
            //    ItemReport.Clear();
            //    BuyerReport.Clear();
            //    WalletHistories.Clear();
            //    iConcatedEntities = "";

            //    List<IEnumerable<DisplayReport>> currentStation = new List<IEnumerable<DisplayReport>>();
            //    List<IEnumerable<DisplayReport>> currentItem = new List<IEnumerable<DisplayReport>>();
            //    List<IEnumerable<DisplayReport>> currentBuyer = new List<IEnumerable<DisplayReport>>();


            //    Func<Transactions, bool> filter = (t) => true;
            //    if (ApplyStartFilter && ApplyEndFilter)
            //        filter = (t) => t.Date >= StartDate && t.Date <= EndDate;
            //    else if (ApplyStartFilter)
            //        filter = (t) => t.Date >= StartDate;
            //    else
            //        filter = (t) => t.Date <= EndDate;

            //    //filter
            //    Func<WalletHistories, bool> historyFilter = null;
            //    if (ApplyStartFilter && ApplyEndFilter)
            //        historyFilter = (wh) => wh.Date >= StartDate && wh.Date <= EndDate;
            //    else if (ApplyStartFilter)
            //        historyFilter = (wh) => wh.Date >= StartDate;
            //    else
            //        historyFilter = (wh) => wh.Date <= EndDate;

            //    foreach (Entities e in Entities.Where(s => s.IsSelected))
            //    {
            //        //filtered: wt => wt.TransactionDateTime.Date >= iFromDate && wt.TransactionType == WalletTransactionType.Sell

            //        iConcatedEntities += e.Name + ",";

            //        var filteredTransactions = e.Wallets.SelectMany(w => w.Transactions).Where(wt => wt.TransactionType == (long)TransactionType.Sell).Where(filter);

            //        var stationData = this.GroupedTransactions(filteredTransactions, t => t.StationName).ToList();
            //        var itemData = this.GroupedTransactions(filteredTransactions, t => t.TypeName).ToList();
            //        var buyerData = this.GroupedTransactions(filteredTransactions, t => t.ClientName).ToList();

            //        currentStation.Add(stationData);
            //        currentItem.Add(itemData);
            //        currentBuyer.Add(buyerData);


            //        //wallet history
            //        List<DisplayWalletHistory> cache = new List<DisplayWalletHistory>();

            //        foreach (Wallets w in e.Wallets)
            //        {
            //            var dwh = new DisplayWalletHistory()
            //            {
            //                Name = w.DisplayName,
            //                Histories = w.WalletHistory.Where(historyFilter).Select(wh => new DisplaySingleHistory() { Date = wh.Date, Balance = wh.Balance }).ToList()
            //            };
            //            cache.Add(dwh);
            //        }
            //        WalletHistories.AddRange(cache);


            //        /*            IEnumerable<ReportChartItem> reportData =
            //        from wt in filteredWalletTransactions
            //        group wt by wt.TypeName into g
            //        select new ReportChartItem
            //        {
            //            Label = string.Format(
            //                "{0} x{1}", 
            //                g.Key, 
            //                g.Sum(gi => gi.Quantity)),
            //            GrossSales = Math.Round(g.Sum(gi => (gi.Price * gi.Quantity) / 1000000), 2),
            //            PureProfit = Math.Round(g.Sum(gi => ((gi.Price  - gi.SalesTax - (this.iActivateTransactionLimit ? Analysis.Products.GetProductAverageBuyPrice(walletTransactions, gi.TypeID, this.iTransactionTimeLimit) : Analysis.Products.GetProductAverageBuyPrice(walletTransactions, gi.TypeID))) * gi.Quantity) / 1000000), 2),
            //            SalesTax = Math.Round(g.Sum(gi => gi.SalesTax * gi.Quantity / 1000000), 2)
            //        };*/
            //    }
            //    if (iConcatedEntities.Length > 0)
            //        iConcatedEntities = iConcatedEntities.Substring(0, iConcatedEntities.Length - 1);

            //    RaisePropertyChanged("ConcatedEntities");

            //    StationReport.AddRange(this.Combine(currentStation).OrderBy(d => d.PureProfit));
            //    ItemReport.AddRange(this.Combine(currentItem).OrderBy(d => d.PureProfit));
            //    BuyerReport.AddRange(this.Combine(currentBuyer).OrderBy(d => d.PureProfit));

            //    iModel.Connection.Close();

            //    this.Updating = false;
            //}
        }

        private void RefreshEntities()
        {
            lock (iUpdaterLock)
            {
                var entitiesCache = Entities.ToList();

                Entities.Clear();

                List<Selectable<Entities>> creationCache = new List<Selectable<Entities>>();
                
                foreach (Entities e in iModel.Entity.OfType<Characters>().ToList())
                {
                    bool selected = false;
                    var prevEntity = entitiesCache.FirstOrDefault(s => s.Item.ID == e.ID);
                    if (prevEntity != null)
                        selected = prevEntity.IsSelected;

                    Selectable<Entities> cache = new Selectable<Entities>(e, selected);
                    creationCache.Add(cache);
                }
                foreach (Entities e in iModel.Entity.OfType<Corporations>().Where(c => !c.Npc).ToList())
                {
                    bool selected = false;
                    var prevEntity = entitiesCache.FirstOrDefault(s => s.Item.ID == e.ID);
                    if (prevEntity != null)
                        selected = prevEntity.IsSelected;

                    Selectable<Entities> cache = new Selectable<Entities>(e, selected);
                    creationCache.Add(cache);
                }

                Entities.AddRange(creationCache);
            }
        }

        public event EventHandler<EntitiesUpdatedEventArgs<long>> ChildRefresh;
        public event EventHandler ChildCancel;

        private void RaiseChildRefresh()
        {
            var handler = ChildRefresh;

            if (handler != null)
            {
                 handler(this, new EntitiesUpdatedEventArgs<long>(Entities.Where(s => s.IsSelected).Select(s => s.Item.ID)));
            }
        }
        private void RaiseChildCancel()
        {
            var handler = ChildCancel;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }


 
        public void DataIncoming(object sender, Services.EntitiesUpdatedEventArgs e)
        {
            RefreshEntities();

            if (e.UpdatedEntities.Any(en => Entities.Where(ce => ce.IsSelected).Any(ce => ce.Item.Name == en.Name)))
                Refresh();
        }
    }
}
