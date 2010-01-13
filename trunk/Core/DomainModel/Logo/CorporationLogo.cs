﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DomainModel
{
    public class CorporationLogo : IGenericObject<CorporationLogo>
    {
        public int ID { get; set; }
        public int Shape1 { get; set; }
        public int Shape2 { get; set; }
        public int Shape3 { get; set; }
        public int Color1 { get; set; }
        public int Color2 { get; set; }
        public int Color3 { get; set; }

        #region Implementation of IGenericObject<CorporationLogo>

        public IEqualityComparer<CorporationLogo> GetComparer()
        {
            return new CorporationLogoComparer();
        }

        #endregion
    }
}