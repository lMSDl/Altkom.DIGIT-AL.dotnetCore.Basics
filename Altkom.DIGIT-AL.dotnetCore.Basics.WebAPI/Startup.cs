using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.FakeServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.FakeServices.Models;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(options => {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
                options.SerializerSettings.Converters.Add(new StringEnumConverter(camelCaseText: true));
            })
            .AddXmlSerializerFormatters();

            services.AddSingleton<CustomerFaker>()
            .AddSingleton<ProductFaker>()
            .AddSingleton<ICustomersService>(x => new FakeCustomersService(x.GetService<CustomerFaker>(), int.Parse(Configuration["FakerCount"])))
            .AddSingleton<IProductsService>(x => new FakeProductsService(x.GetService<ProductFaker>(), int.Parse(Configuration["FakerCount"])))
            .AddSingleton<OrderFaker>()
            .AddSingleton<IOrdersService>(x => new FakeOrdersService(x.GetService<OrderFaker>(), int.Parse(Configuration["FakerCount"])));
         
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(AuthService.Key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddScoped<IAuthService, AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseMiddleware<ExceptionMiddleware>();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                // app.UseExceptionHandler(appError => {
                //     appError.Run(async context => {
                //         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //         context.Response.ContentType = "application/json";
                //         var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();
                //         if(contextFeatures != null) {
                //             await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails{
                //                 StatusCode = context.Response.StatusCode,
                //                 Message = contextFeatures.Error.Message} ));
                //         }
                //     });
                // });
            }
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
