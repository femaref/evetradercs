using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Core.ClassExtenders;
using Core.DomainModel;
using Core.Network.EveApi.Entities;

namespace Core.Network.EveApi.Requests
{
    public class MarketOrdersRequest : EveApiEntityRequest<IEnumerable<MarketOrder>>
    {
        protected override EveApiResourceType ResourceType
        {
            get 
            {
                return EveApiResourceType.MarketOrders;
            }
        }

        public MarketOrdersRequest(IAccount account) : base (account)
        {
        }

        public override IEnumerable<MarketOrder> Request()
        {
            return this.Parse(base.GetResponseXml());
        }

        private IEnumerable<MarketOrder> Parse(XDocument document)
        {
            if (this.ErrorCode != 0)
                return null;

            var root = document.Element("eveapi").Element("result").Element("rowset").Elements();

            var orders = root.Select(r => new MarketOrder
                             {
                                 ID = r.Attribute("orderID").Value.ToInt32(),
                                 EntityID = r.Attribute("charID").Value.ToInt32(),
                                 StationID = r.Attribute("stationID").Value.ToInt32(),
                                 VolumeEntered = r.Attribute("volEntered").Value.ToInt32(),
                                 VolumeRemaining = r.Attribute("volRemaining").Value.ToInt32(),
                                 VolumeMinimum = r.Attribute("minVolume").Value.ToInt32(),
                                 OrderState = (MarketOrderState) Enum.Parse(typeof(MarketOrderState), r.Attribute("orderState").Value),
                                 TypeID = r.Attribute("typeID").Value.ToInt32(),
                                 Range = r.Attribute("range").Value.ToInt32(),
                                 AccountKey = r.Attribute("accountKey").Value.ToInt32(),
                                 Duration = r.Attribute("duration").Value.ToInt32(),
                                 Escrow = r.Attribute("escrow").Value.ToDouble(),
                                 Price = r.Attribute("price").Value.ToDouble(),
                                 Type = r.Attribute("bid").Value == "0" ? MarketOrderType.Sell : MarketOrderType.Buy,
                                 Issued = r.Attribute("issued").Value.ToDateTime()
                             });

            return orders;

        }
    }
}
