﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EveTrader.Core.Model.Trader;
using System.Xml.Linq;

namespace EveTrader.Core.Services
{
    public interface IDatabaseExportService
    {
        XDocument Export(Accounts a);
        event EventHandler Started;
        event EventHandler Completed;
    }
}