using AutoMapper;
using RoboBank.Account.Application;
using RoboBank.Account.Service.Models;

namespace RoboBank.Account.Service
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TransferModel, TransferInfo>();
                cfg.CreateMap<AmountModel, AmountInfo>();
                cfg.CreateMap<AccountInfo, AccountModel>();
                cfg.CreateMap<AmountInfo, AmountModel>();
                cfg.CreateMap<Domain.Account, AccountInfo>();
            });
        }
    }
}