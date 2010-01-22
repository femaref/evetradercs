﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.ClassExtenders;
using Core.DomainModel;

namespace Core.Network.EveApi
{
    public abstract class EveApiCorporationResourceRequest<T> : EveApiResourceRequest
    {
        protected int CharacterId { get; set; }

        public int ErrorCode
        {
            get
            {
                if (this.CachedResponseXml.Descendants().Where(x => x.Name == "error").Count() > 0)
                    return this.CachedResponseXml.Descendants("error").First().Attribute("code").Value.ToInt32();
                else
                    return 0;
            }
        }

        protected override IList<ResourceRequestParameter> Parameters
        {
            get
            {
                IList<ResourceRequestParameter> parameters = base.Parameters;
                parameters.Add(new ResourceRequestParameter { Name = "characterId", Value = this.CharacterId.ToString() });

                return parameters;
            }
        }
        protected override Uri Uri
        {
            get
            {
                return new Uri(
                    string.Format(
                        this.RequestUrlTemplate,
                        EveApiResourceFrom.Corporation.StringValue(),
                        this.ResourceType.StringValue()));
            }
        }

        protected EveApiCorporationResourceRequest(IAccount input)
            : base(input.ApiData.UserID, input.ApiData.ApiKey)
        {
            this.CharacterId = input.ApiData.CharacterID;
        }

        protected EveApiCorporationResourceRequest(int accountId, string apiKey, int characterId)
            : base(accountId, apiKey)
        {
            this.CharacterId = characterId;
        }

        public abstract T Request();
    }
}