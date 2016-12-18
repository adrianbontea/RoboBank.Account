using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.ServiceBus.Messaging;
using RoboBank.Account.Application.Adapters;
using RoboBank.Account.Application.Ports;
using RoboBank.Account.Domain.Adapters;
using RoboBank.Account.Domain.Ports;

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
            container.RegisterType<IAccountRepository, AccountRepository>();
            container.RegisterType<ICardRepository, CardRepository>();
            container.RegisterType<IMapper, Mapper>();
            container.RegisterType<IExchangeRatesService, FixerExchangeRatesService>();
            container.RegisterType<IEventService, ServiceBusEventService>();
            container.RegisterType<UnitOfWork>(new HierarchicalLifetimeManager());

            var connectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
            var queueName = ConfigurationManager.AppSettings["QueueName"];

            var queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
            container.RegisterInstance(queueClient);
        }
    }
}