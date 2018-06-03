using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NasaNeo.Business.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NasaNeo.Business.NasaApi;

namespace NasaNeo.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //figure out how to determine dev vs not dev here to use dev conn or keyvault conn
            var environment = Configuration["Environment"];

            //services.AddTransient<INasaNeoRepo>();
            services.AddSingleton<INasaNeoRepo>(new NasaApiLiveRepo(Configuration));

            //if(environment == "Development")
            //{
            //    services.AddDbContext<MemoryGameContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("DevMemoryGame")));
            //}
            //else
            //{
            //    services.AddDbContext<MemoryGameContext>(options =>
            //        options.UseSqlServer(Configuration["appSettings:connectionStrings:memory"]));
            //}
            services.AddMvc();            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
