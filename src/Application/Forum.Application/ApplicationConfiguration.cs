﻿using AutoMapper;
using Forum.Application.Common;
using Forum.Application.Common.Behaviours;
using Forum.Application.Identity.Commands.LoginUser;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Forum.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .Configure<ApplicationSettings>(
                    configuration.GetSection(nameof(ApplicationSettings)),
                    options => options.BindNonPublicProperties = true)
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddEventHandlers()
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                 .AddEventHandlers();

        private static IServiceCollection AddEventHandlers(this IServiceCollection services)
            => services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
    }
}
