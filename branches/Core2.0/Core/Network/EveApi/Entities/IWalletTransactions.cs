﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainModel;

namespace Core.Network.EveApi.Entities
{
    public interface IWalletTransactions : ISubEntity
    {
        int Key { get; set; }
        List<WalletTransaction> Transactions { get; set; }
        DateTime NextWalletTransactionsUpdateTime { get; set; }
    }
}