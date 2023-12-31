﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using MedicalQueueApi.Data;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace MedicalQueueApi {
    public class Startup : StartupBase {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        override public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<ApplicationContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("PostgresqlConnection")));

            services.AddCors(o => o.AddPolicy("CorsAllowAny", builder => {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options => {
                   options.TokenValidationParameters = new TokenValidationParameters {
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                   };
               });

            services.AddMvc().AddNewtonsoftJson(options => {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddControllers();
            //services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            /*
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });
            });
            */
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        override public void Configure(IApplicationBuilder app) {
            app.UseAuthentication();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            //app.UseMvc();
        }
    }
}