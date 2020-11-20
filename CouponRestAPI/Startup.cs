using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CouponRestAPI.Models;
using CouponRestAPI.Data;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace CouponRestAPI
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
            services.Configure<CouponApiSettings>(
                Configuration.GetSection(nameof(CouponApiSettings)));

            services.AddSingleton<ICouponApiSettings>(sp =>
                sp.GetRequiredService<IOptions<CouponApiSettings>>().Value);

            services.AddSingleton<CouponService>();

            services.AddControllers().AddNewtonsoftJson(s => {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //use MockCouponRepo 
            //services.AddScoped<ICouponRepo, MockCouponRepo>();

            //use MongoCommanderRepo 
            services.AddScoped<ICouponRepo, MongoCouponRepo>();

            //use SQLCommanderRepo 
            //services.AddScoped<ICouponRepo, SQLCouponRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
