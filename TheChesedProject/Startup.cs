using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TheChesedProject.Models;
using SendGrid;
using Newtonsoft.Json.Serialization;
using Braintree;
using System.Globalization;

namespace TheChesedProject
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


            string tcpConnectionString = Configuration.GetConnectionString("TCP");

            services.AddDbContext<Models.TCPDbContext>(opt => opt.UseSqlServer(tcpConnectionString));
           

            services.AddIdentity<TCPUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<Models.TCPDbContext>()
                .AddDefaultTokenProviders();

            

             services.ConfigureApplicationCookie(options =>
             {
                 // Cookie settings
                 options.Cookie.HttpOnly = true;
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                 // If the LoginPath isn't set, ASP.NET Core defaults 
                 // the path to /Account/Login.
                 options.LoginPath = "/Account/Login";
                 // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                 // the path to /Account/AccessDenied.
                 options.AccessDeniedPath = "/Account/AccessDenied";
                 options.SlidingExpiration = true;
             });
            /*
            services.AddTransient<SendGrid.SendGridClient>((x) =>
            {
                return new SendGrid.SendGridClient("api_key_here");
            });

            services.AddTransient<SendGrid.SendGridClient>((x) =>
            {
                return new SendGrid.SendGridClient(Configuration["sendgrid"]);
            });
            */
            services
                .AddMvc()
                .AddJsonOptions(
                    o =>
                    {
                        o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    });

           services.AddTransient((x) => { return new EmailService(Configuration["SendGridKey"]); });

            
            services.AddTransient((x) => {
                return new Braintree.BraintreeGateway(
                    Configuration["BraintreeEnvironment"],
                    Configuration["BraintreeMerchantId"],
                    Configuration["BraintreePublicKey"],
                    Configuration["BraintreePrivateKey"]);
            });

            services.AddTransient((x) =>
            {
                SmartyStreets.ClientBuilder builder = new SmartyStreets.ClientBuilder(Configuration["SmartyStreetsAuthId"], Configuration["SmartyStreetsAuthToken"]);
                return builder.BuildUsStreetApiClient();
            });
        }

       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, TCPDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            DbInitializer.Initialize(db);
            
        }
    }
}
