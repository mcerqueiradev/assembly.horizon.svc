﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assembly.Horizon.Application;

public static class DepedencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp",
                builder => builder.WithOrigins("http://localhost:4200")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });

        services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        });

        return services;
    }

}
