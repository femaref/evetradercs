﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.IO;
using EveTrader.Core.ClassExtenders;

namespace EveTrader.Core.Network.Requests.CCP
{
    public abstract class ApiRequestBase<T>
    {
        protected Dictionary<string, string> iData = new Dictionary<string, string>();

        protected readonly ApiRequestTarget iTarget;

        public ApiRequestBase(ApiRequestTarget target)
        {
            iTarget = target;
        }

        public XDocument CachedResponseXml
        {
            get { return GetRequestXml(); }
        }

        private XDocument iCachedResponseXml = null;

        private XDocument GetRequestXml()
        {
            if (iCachedResponseXml != null)
                return iCachedResponseXml;

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(Identifier);

            req.UserAgent = "EveTrader/1.2.5";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";


            byte[] postData = Encoding.Default.GetBytes(this.Data);
            req.ContentLength = postData.Length;

            Stream s = req.GetRequestStream();
            s.Write(postData, 0, postData.Length);
            s.Close();

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            using (StreamReader reader = new StreamReader(res.GetResponseStream()))
            {
                string output = reader.ReadToEnd();
                iCachedResponseXml =  XDocument.Parse(output);
                return iCachedResponseXml;
            }
        }

        private const string BaseIdentifier = @"http://api.eve-online.com/{0}/{1}.xml.aspx";

        public ApiRequestTarget Target
        {
            get { return iTarget; }
        }
        public abstract ApiRequestPage Page { get; }

        public Uri Identifier
        {
            get
            {
                return new Uri(
                    string.Format(
                                BaseIdentifier,
                                Target.StringValue(),
                                Page.StringValue())
                    );
            }
        }

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

        protected virtual string Data
        {
            get
            {
                return "";
            }
        }

        public T Request()
        {
            return this.Parse(this.GetRequestXml());
        }

        protected abstract T Parse(XDocument document);
    }
}
