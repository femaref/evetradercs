using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.DomainModel;

namespace Core.ClassExtenders
{
    public static class WalletTransactionExtender
    {
        public static IEnumerable<WalletJournalRecord> MatchWalletJournalRecords(this WalletTransaction walletTransaction, IEnumerable<WalletJournalRecord> walletJournalRecords)
        {
            try
            {
                return walletJournalRecords.Where(wjr => wjr.Date == walletTransaction.TransactionDateTime);
            }
            catch(InvalidOperationException)
            {
                return new Collection<WalletJournalRecord>();
            }
        }
    }
}
