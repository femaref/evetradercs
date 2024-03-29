
// <copyright file="DtoGenerator.tt" company="Elvian Empire">
//  Copyright � Elvian Empire. All Rights Reserved.
// </copyright>
// Code generated by a template
using System;
using System.Collections.Generic;
using EveTrader.Core.Services;

namespace EveTrader.Core.Model.Trader
{
    public class TraderMappings : IMappingCreator
    {
        public void CreateMappings()
        {
        	AutoMapper.Mapper.CreateMap<Wallets, WalletsDto>();
        	AutoMapper.Mapper.CreateMap<WalletsDto, Wallets>();
        	AutoMapper.Mapper.CreateMap<Accounts, AccountsDto>();
        	AutoMapper.Mapper.CreateMap<AccountsDto, Accounts>();
        	AutoMapper.Mapper.CreateMap<ApiCache, ApiCacheDto>();
        	AutoMapper.Mapper.CreateMap<ApiCacheDto, ApiCache>();
        	AutoMapper.Mapper.CreateMap<Journal, JournalDto>();
        	AutoMapper.Mapper.CreateMap<JournalDto, Journal>();
        	AutoMapper.Mapper.CreateMap<ApiJournal, ApiJournalDto>();
        	AutoMapper.Mapper.CreateMap<ApiJournalDto, ApiJournal>();
        	AutoMapper.Mapper.CreateMap<Transactions, TransactionsDto>();
        	AutoMapper.Mapper.CreateMap<TransactionsDto, Transactions>();
        	AutoMapper.Mapper.CreateMap<ApiTransactions, ApiTransactionsDto>();
        	AutoMapper.Mapper.CreateMap<ApiTransactionsDto, ApiTransactions>();
        	AutoMapper.Mapper.CreateMap<ApplicationLog, ApplicationLogDto>();
        	AutoMapper.Mapper.CreateMap<ApplicationLogDto, ApplicationLog>();
        	AutoMapper.Mapper.CreateMap<CachedPriceInfos, CachedPriceInfosDto>();
        	AutoMapper.Mapper.CreateMap<CachedPriceInfosDto, CachedPriceInfos>();
        	AutoMapper.Mapper.CreateMap<Entities, EntitiesDto>();
        	AutoMapper.Mapper.CreateMap<EntitiesDto, Entities>();
        	AutoMapper.Mapper.CreateMap<Characters, CharactersDto>();
        	AutoMapper.Mapper.CreateMap<CharactersDto, Characters>();
        	AutoMapper.Mapper.CreateMap<Corporations, CorporationsDto>();
        	AutoMapper.Mapper.CreateMap<CorporationsDto, Corporations>();
        	AutoMapper.Mapper.CreateMap<CustomJournal, CustomJournalDto>();
        	AutoMapper.Mapper.CreateMap<CustomJournalDto, CustomJournal>();
        	AutoMapper.Mapper.CreateMap<CustomTransactions, CustomTransactionsDto>();
        	AutoMapper.Mapper.CreateMap<CustomTransactionsDto, CustomTransactions>();
        	AutoMapper.Mapper.CreateMap<MarketOrders, MarketOrdersDto>();
        	AutoMapper.Mapper.CreateMap<MarketOrdersDto, MarketOrders>();
        	AutoMapper.Mapper.CreateMap<RefTypes, RefTypesDto>();
        	AutoMapper.Mapper.CreateMap<RefTypesDto, RefTypes>();
        	AutoMapper.Mapper.CreateMap<WalletHistories, WalletHistoriesDto>();
        	AutoMapper.Mapper.CreateMap<WalletHistoriesDto, WalletHistories>();
        }
    }
}
