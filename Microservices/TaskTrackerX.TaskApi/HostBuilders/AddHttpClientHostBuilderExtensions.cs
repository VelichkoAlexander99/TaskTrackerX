﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskTrackerX.TaskApi.Models.Options;
using TaskTrackerX.TaskApi.Services;

namespace TaskTrackerX.TaskApi.HostBuilders
{
    public static class AddHttpClientHostBuilderExtensions
    {
        public static void AddHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            var microservicesConfig = configuration
                .GetSection("SettingOptions")
                .Get<SettingOptions>();

            if (microservicesConfig == null)
                throw new ArgumentNullException(nameof(microservicesConfig));

            services.AddHttpContextAccessor();

            services.AddHttpClient(microservicesConfig.AuthApi.ServiceName, client =>
            {
                client.BaseAddress = new Uri(microservicesConfig.AuthApi.BaseUrl);
            }).AddHttpMessageHandler<BearerTokenHandler>();
        }
    }
}
