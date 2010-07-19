﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveTrader.Core.Model
{
    public interface ISettingsProvider
    {
        bool AutoUpdate { get; set; }
        bool HideExpired { get; set; }
        TimeSpan JournalTimeframe { get; set; }
    }
}
