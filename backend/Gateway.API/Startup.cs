using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;

namespace Gateway.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public string IdentityServerAuthority => Configuration["IdentityServer:Authority"];
        public string IdentityServerApiName => Configuration["IdentityServer:ApiName"];
        public string IdentityServerAPISecret => Configuration["IdentityServer:ApiSecret"];
        public bool IdentityServerRequireHttpsMetadata => Convert.ToBoolean(Configuration["IdentityServer:RequireHttpsMetadata"]);
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("document", new OpenApiInfo { Title = "API Documents", Version = "v1" });
            });

            var authenticationProviderKey = "OcelotClient";
            services.AddAuthentication()
                .AddIdentityServerAuthentication(authenticationProviderKey, o =>
                {
                    o.Authority = IdentityServerAuthority;
                    o.RequireHttpsMetadata = IdentityServerRequireHttpsMetadata;
                    o.SupportedTokens = SupportedTokens.Reference;
                    o.ApiName = IdentityServerApiName;
                    o.ApiSecret = IdentityServerAPISecret;
                });
            services.AddOcelot();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var swaggerDocs = new SwaggerDocs();
            Configuration.GetSection("SwaggerDocs").Bind(swaggerDocs);
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                swaggerDocs.Items.ForEach(doc =>
                {
                    o.SwaggerEndpoint(doc.Endpoint, doc.Name);
                });
            });

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.UseOcelot().Wait();
        }
    }
}
