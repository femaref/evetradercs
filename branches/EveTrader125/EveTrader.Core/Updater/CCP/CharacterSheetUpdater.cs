﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EveTrader.Core.Model;
using EveTrader.Core.Network.Requests.CCP;
using System.ComponentModel.Composition;

namespace EveTrader.Core.Updater.CCP
{
    [Export(typeof(ICharacterSheetUpdater))]
    public class CharacterSheetUpdater : UpdaterBase<Characters>, ICharacterSheetUpdater
    {
        private readonly CorporationUpdater iCorporationSheetUpdater;

        [ImportingConstructor]
        public CharacterSheetUpdater(TraderModel tm, CorporationUpdater corpSheetUpdater)
            : base(tm)
        {
            iCorporationSheetUpdater = corpSheetUpdater;
        }

        protected override bool InnerUpdate(Characters entity)
        {
            CharacterSheetRequest req = new CharacterSheetRequest(entity.Account, entity.ID);

            Characters c = req.Request();

            entity.Name = c.Name;
            entity.Race = c.Race;
            entity.Balance = c.Balance;
            entity.Bloodline = c.Bloodline;
            entity.Gender = c.Gender;
            if (entity.Corporation.ID != c.Corporation.ID)
            {
                iCorporationSheetUpdater.Update(c.Corporation);
                entity.Corporation = c.Corporation;
            }

            iModel.SaveChanges();

            return true;
        }
    }
}
