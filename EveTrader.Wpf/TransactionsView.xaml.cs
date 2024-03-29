﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EveTrader.Core.Visual.View;
using System.ComponentModel.Composition;
using EveTrader.Core.Model.Trader;

namespace EveTrader.Wpf
{
    /// <summary>
    /// Interaction logic for TransactionsView.xaml
    /// </summary>
    [Export(typeof(ITransactionsView))]
    public partial class TransactionsView : UserControl, ITransactionsView
    {
        public TransactionsView()
        {
            InitializeComponent();
            var x = (CollectionViewSource)this.Resources["iGroupedTransactions"];
            x.GroupDescriptions.Add(new PropertyGroupDescription("Date"));
        }

        #region ITransactionsView Members

        public event EventHandler<EntitySelectionChangedEventArgs<Wallets>> EntitySelectionChanged;

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
                RaiseEntitySelectionChanged(e.AddedItems.Cast<Wallets>().First());
        }
        private void RaiseEntitySelectionChanged(Wallets selection)
        {
            var handler = EntitySelectionChanged;
            if (handler != null)
                handler(this, new EntitySelectionChangedEventArgs<Wallets>(selection));
        }

        #endregion

        #region IExtendedView Members

        public void Invoke(Action action)
        {
            Dispatcher.Invoke(action);
        }

        public void BeginInvoke(Action action)
        {
            Dispatcher.BeginInvoke(action);
        }

        #endregion
    }
}
