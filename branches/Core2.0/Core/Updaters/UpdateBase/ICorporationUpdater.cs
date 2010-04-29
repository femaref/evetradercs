﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainModel;
using Core.Network.EveApi.Entities;
using Core.Updaters;

namespace Core.Updaters.UpdateBase
{
    public interface ICorporationUpdater<T> : IEntityUpdater<T> where T:IEntity
    {
        bool UpdateCorporation(Corporation corporation);
    }

    public interface ICorporationUpdater
    {
        bool UpdateCorporation(Corporation corporation);
    }
}