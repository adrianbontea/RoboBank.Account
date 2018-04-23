using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.ServiceBus.Messaging;
using RoboBank.Account.Application;
using RoboBank.Account.Application.Adapters;
using RoboBank.Account.Domain;
using RoboBank.Account.Domain.Adapters;

namespace RoboBank.Account.Service
{
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<AccountApplicationService.IAccountRepository, AccountRepository>();
            container.RegisterType<AccountApplicationService.ICardRepository, CardRepository>();
            container.RegisterType<AccountApplicationService.IMapper, Mapper>();
            container.RegisterType<FundsTransferService.IExchangeRatesService, FixerExchangeRatesService>();
            container.RegisterType<AccountApplicationService.IEventService, ServiceBusEventService>();
            container.RegisterType<UnitOfWork>(new HierarchicalLifetimeManager());

            var connectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
            var queueName = ConfigurationManager.AppSettings["QueueName"];

            var queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
            container.RegisterInstance(queueClient);
        }
    }
}