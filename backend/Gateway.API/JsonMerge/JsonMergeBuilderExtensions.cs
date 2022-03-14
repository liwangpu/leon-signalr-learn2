using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.JsonMerge
{
    public static class JsonMergeBuilderExtensions
    {
        public static IConfigurationBuilder AddJsonMergeFiles(this IConfigurationBuilder builder, Action<JsonMergeOptions> configure)
        {
            var options = new JsonMergeOptions();
            configure?.Invoke(options);
            builder.Add(new JsonMergeSource(options));
            return builder;
        }
    }
}
