using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using TechFood.Application;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.Orders.Queries;
using TechFood.Domain.Enums;
using TechFood.Domain.Repositories;
using TechFood.Infra.Payments.MercadoPago;
using TechFood.Infra.Persistence.Contexts;
using TechFood.Infra.Persistence.ImageStorage;
using TechFood.Infra.Persistence.Queries;
using TechFood.Infra.Persistence.Repositories;
using TechFood.Shared.Infra.Extensions;

namespace TechFood.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddSharedInfra<OrderContext>(new InfraOptions
        {
            DbContext = (serviceProvider, dbOptions) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                dbOptions.UseSqlServer(config.GetConnectionString("DataBaseConection"));
            },
            AssemblyLoad = typeof(DependecyInjection).Assembly
        });

        //Context
        //services.AddScoped<TechFoodContext>();
        //services.AddDbContext<TechFoodContext>((serviceProvider, options) =>
        //{
        //    var config = serviceProvider.GetRequiredService<IConfiguration>();

        //    options.UseSqlServer(config.GetConnectionString("DataBaseConection"));
        //});

        ////UoW
        //services.AddScoped<IUnitOfWorkTransaction, UnitOfWorkTransaction>();
        //services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<TechFoodContext>());

        ////DomainEvents
        //services.AddScoped<IDomainEventStore>(serviceProvider => serviceProvider.GetRequiredService<TechFoodContext>());

        //Data
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPreparationRepository, PreparationRepository>();

        //Queries
        services.AddScoped<IOrderQueryProvider, OrderQueryProvider>();

        services.AddScoped<IImageStorageService, LocalDiskImageStorageService>();

        // Payments
        services.AddOptions<MercadoPagoOptions>()
            .Configure<IConfiguration>((options, config) =>
            {
                var configSection = config.GetSection(MercadoPagoOptions.SectionName);
                configSection.Bind(options);
            });

        services.AddKeyedTransient<IPaymentService, MercadoPagoPaymentService>(PaymentType.MercadoPago);

        services.AddHttpClient(MercadoPagoOptions.ClientName, (serviceProvider, client) =>
        {
            client.BaseAddress = new Uri(MercadoPagoOptions.BaseAddress);
            client.DefaultRequestHeaders.Add("X-Idempotency-Key", Guid.NewGuid().ToString());

            client.DefaultRequestHeaders.Authorization = new("Bearer", serviceProvider.GetRequiredService<IOptions<MercadoPagoOptions>>().Value.AccessToken);
        });

        ////MediatR
        //services.AddMediatR(typeof(DependecyInjection));

        //var mediatR = services.First(s => s.ServiceType == typeof(IMediator));

        //services.Replace(ServiceDescriptor.Transient<IMediator, EventualConsistency.Mediator>());
        //services.Add(
        //    new ServiceDescriptor(
        //        mediatR.ServiceType,
        //        EventualConsistency.Mediator.ServiceKey,
        //        mediatR.ImplementationType!,
        //        mediatR.Lifetime));

        return services;
    }
}
