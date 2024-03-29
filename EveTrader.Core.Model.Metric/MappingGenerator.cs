
// <copyright file="DtoGenerator.tt" company="Elvian Empire">
//  Copyright � Elvian Empire. All Rights Reserved.
// </copyright>
// Code generated by a template
using System;
using System.Collections.Generic;
using EveTrader.Core.Services;

namespace EveTrader.Core.Model.Metric
{
    public class MetricMappings : IMappingCreator
    {
        public void CreateMappings()
        {
        AutoMapper.Mapper.CreateMap<Cache, CacheDto>();
        AutoMapper.Mapper.CreateMap<CacheDto, Cache>();
        AutoMapper.Mapper.CreateMap<ItemPrices, ItemPricesDto>();
        AutoMapper.Mapper.CreateMap<ItemPricesDto, ItemPrices>();
        }
    }
}
