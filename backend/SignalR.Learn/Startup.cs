using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Base.API;
//using Microsoft.OpenApi.Models;


namespace Backend
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public string IdentityServerIssuerUri => Configuration["IdentityServer:IssuerUri"];

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddPrifileContext();

            //#region Swagger
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("oss", new OpenApiInfo { Title = "OSS API", Version = "v1" });
            //});
            //#endregion
            services.AddSignalR();
            //services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
