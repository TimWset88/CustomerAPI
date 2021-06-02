using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Configuration
{
    public static class ConfigurationStoreExtensions
    {
        public static IConfigurationBuilder AddSqlServerConfigurationProvider(this IConfigurationBuilder builder)
        {
            var source = new ConfigurationStore();
            builder.Add(source);
            return builder;
        }
    }
}
