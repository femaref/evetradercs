﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveTrader.Core.Updater.CCP
{
    public class UpdaterFailedException : Exception
    {

        public UpdaterFailedException(string message, Exception ex)
            : base(message, ex)
        {
        }

    }
}
