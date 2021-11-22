using System;
using Channel.Users.Application.Commands;
using Channel.Users.Application.Commands.GetUsersReport;
using Channel.Users.Domain.Infrastructure;
using Channel.Users.Domain.Reporting.Users;
using Channel.Users.HttpDataProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Channel.Users.Application.Infrastructure.DependencyRegistry
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUsersApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Commands
            services.AddScoped<ICommandHandler<GetUsersReportRequest, GetUsersReportResponse>, GetUsersReportCommandHandler>();
            services.AddScoped<IUsersReportingService, UsersReportingService>();
            services.AddScoped<IUsersReportingDataProvider, UsersReportingHttpDataProvider>();

            // Http client
            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(4, retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            services.AddHttpClient("HttpDataProvider")
                .AddPolicyHandler(retryPolicy);

            // Settings
            services.Configure<HttpDataProviderSettings>(settings =>
            {
                settings.Url = configuration.GetSection("HttpDataProviderSettings:Url")?.Value;
            });

            return services;
        }
    }
}
