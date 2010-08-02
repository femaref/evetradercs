﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Waf.Applications;
using EveTrader.Core.View;
using EveTrader.Core.Model;
using System.Collections.ObjectModel;
using MoreLinq;
using System.ComponentModel.Composition;
using EveTrader.Core.ViewModel.Display;
using EveTrader.Core.Collections.ObjectModel;

namespace EveTrader.Core.ViewModel
{
    [Export]
    public class PriceCacheViewModel : ViewModel<IPriceCacheView>, IRefreshableViewModel
    {
        private readonly TraderModel iModel;
        private readonly StaticModel iStaticData;

        public SmartObservableCollection<DisplayPriceCache> Prices { get; set; }

        [ImportingConstructor]
        public PriceCacheViewModel(IPriceCacheView view, TraderModel tm, StaticModel sm)
            : base(view)
        {
            iModel = tm;
            iStaticData = sm;

            Prices = new SmartObservableCollection<DisplayPriceCache>(view.BeginInvoke);

            Refresh();
        }

        public void Refresh()
        {
            Prices.Clear();

            var x = iModel.CachedPriceInfo.Select(c => new DisplayPriceCache()
                {
                    TypeID = c.TypeID,
                    BuyPrice = c.BuyPrice,
                    SellPrice = c.SellPrice
                }).ToList();
            x.ForEach(c =>
            {
                c.TypeName = iStaticData.invTypes.First(ty => ty.typeID == c.TypeID).typeName;
                
            });
            x.OrderBy(c => c.TypeName).ForEach(c => Prices.Add(c));


        }

        public void DataIncoming(object sender, Controllers.EntitiesUpdatedEventArgs e)
        {
            Refresh();
        }
    }
}
