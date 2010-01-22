﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainModel;
using Core.ClassExtenders;

namespace Core.Network.EveApi.Requests
{
    public class CorporationSheetRequest : EveApiCorporationResourceRequest<Corporation>
    {
        private Account iAccount;

        private Corporation iCorp;

        public CorporationSheetRequest(Corporation account)
            : base(account.ApiData.UserID, account.ApiData.ApiKey, account.ApiData.CharacterID)
        {
            this.iAccount = account.ApiData;
            this.iCorp = account;
        }


        protected override EveApiResourceType ResourceType
        {
            get { return EveApiResourceType.CorporationSheet; }
        }

        public override Corporation Request()
        {
            return this.Parse(base.GetResponseXml());
        }

        private Corporation Parse(System.Xml.Linq.XDocument document)
        {
            var root = document.Element("eveapi").Element("result");

            return new Corporation()
                       {
                           ID = root.Element("corporationID").Value.ToInt32(),
                           Name = root.Element("corporationName").Value,
                           AllianceID = root.Element("allianceID").Value.ToInt32(),
                           AllianceName = root.Element("allianceName") != null ? root.Element("allianceName").Value : "",
                           CeoID = root.Element("ceoID").Value.ToInt32(),
                           CeoName = root.Element("ceoName").Value,
                           Description = root.Element("description").Value,
                           HeadquarterStationID = root.Element("stationID").Value.ToInt32(),
                           HeadquarterStationName = root.Element("stationName").Value,
                           MemberCount = root.Element("memberCount").Value.ToInt32(),
                           MemberLimit = root.Element("memberLimit").Value.ToInt32(),
                           Shares = root.Element("shares").Value.ToInt32(),
                           TaxRate = root.Element("taxRate").Value.ToDouble(),
                           Ticker = root.Element("ticker").Value,
                           Url = root.Element("url").Value,
                           Logo = new CorporationLogo()
                                      {
                                          CorporationID = root.Element("corporationID").Value.ToInt32(),
                                          Color1 = root.Element("logo").Element("color1").Value.ToInt32(),
                                          Color2 = root.Element("logo").Element("color2").Value.ToInt32(),
                                          Color3 = root.Element("logo").Element("color3").Value.ToInt32(),
                                          Shape1 = root.Element("logo").Element("shape1").Value.ToInt32(),
                                          Shape2 = root.Element("logo").Element("shape2").Value.ToInt32(),
                                          Shape3 = root.Element("logo").Element("shape3").Value.ToInt32()

                                      },
                           Divisions =
                               (from x in
                                    root.Elements().Where(
                                    x => x.Name == "rowset" && x.Attribute("name").Value == "divisions").First().
                                    Elements()
                                select
                                    new SerializableKeyValuePair<int, string>(x.Attribute("accountKey").Value.ToInt32(),
                                                                              x.Attribute("description").Value)).ToList(),
                           WalletDivisions =
                               (from x in
                                    root.Elements().Where(
                                    x => x.Name == "rowset" && x.Attribute("name").Value == "walletDivisions").First().
                                    Elements()
                                select
                                    new SerializableKeyValuePair<int, string>(x.Attribute("accountKey").Value.ToInt32(),
                                                                              x.Attribute("description").Value)).ToList()
                       };
        }
    }
}