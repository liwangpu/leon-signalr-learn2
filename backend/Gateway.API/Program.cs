using Gateway.API.JsonMerge;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace Gateway.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, build) =>
            {
                build.SetBasePath(context.HostingEnvironment.ContentRootPath);
                var env = context.HostingEnvironment.EnvironmentName;

                build.AddJsonMergeFiles(options =>
                {
                    options.Files.Add("appsettings.json");
                    options.Files.Add($"appsettings.{env}.json");
                    var folder = $"OcelotConfiguration/{env}";
                    options.Files.Add($"{folder}/base.json");
                    options.Files.Add($"{folder}/ids.json");
                    options.Files.Add($"{folder}/signalr.json");
                })
                .AddEnvironmentVariables();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseNLog();
    }
}
