using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Infra.Data.Context;
using Assembly.Horizon.Infra.Data.Infrastructure;
using Assembly.Horizon.Infra.Data.Infrastructure.Models;
using Assembly.Horizon.Infra.Data.Interceptors;
using Assembly.Horizon.Infra.Data.Repositories;
using Assembly.Horizon.Infra.Data.Uow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Assembly.Horizon.Infra.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(configuration.GetConnectionString("HorizonCS"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IRealtorRepository, RealtorRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IFavoritesRepository, FavoritesRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<INotificationStrategy, HybridNotificationStrategy>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IPropertyVisitRepository, PropertyVisitRepository>();
        services.AddScoped<IEmailService, EmailService>();

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var connectionString = configuration.GetConnectionString("Redis");
            return RedisConfiguration.CreateConnection(connectionString);
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "HorizonCache:";
        });


        services.AddSingleton<IPdfGenerationService>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<PdfGenerationService>>();
            return new PdfGenerationService(
                configuration["PdfOutput:Directory"], // Diretório de saída
                logger,
                configuration // Passando IConfiguration
            );
        });

        return services;
    }

}

