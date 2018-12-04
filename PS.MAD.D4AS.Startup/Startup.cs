using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PS.MAD.D4AS.Startup
{
    public class Startup
    {
        private readonly API.Startup _apiStartup;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this._apiStartup = new API.Startup(configuration);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            this._apiStartup.ConfigureServices(services);

            services.AddScoped<UseCases.Contracts.ITicketRepository, DataAccess.TicketRepository>();
            services.AddScoped<DataAccess.Contracts.IStorage, Storage.AzureStorage>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            this._apiStartup.Configure(app, env);
        }
    }
}
