using System;
using Email;
using Entity.Business.Abstract;
using Entity.Business.Manager;
using Entity.DataAccess.EfDal;
using Entity.Domain.Abstract.IDal;
using IdentityByExamples.CustomTokenProviders;
using IdentityByExamples.Factory;
using Oblivion.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oblivion.Helpers;
using Oblivion.Models.Account;

namespace Oblivion
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
            
            services.AddSingleton<IProductService, ProductManager>();//eger biri productservice isterse ona productmanager i ver
            services.AddSingleton<IProductDal, EfProductDal>();
            services.AddSingleton<ICategoryService, CategoryManager>();
            services.AddSingleton<ICategoryDal, EfCategoryDal>();
            services.AddScoped<ICartService, CartManager>();
            services.AddScoped<ICartSessionHelper, CartSessionHelper>();
            services.AddSingleton<IOrderService, OrderManager>();
            services.AddSingleton<IOrderDal, EfOrderDal>();
            services.AddTransient<IOrderDetailService, OrderDetailManager>();
            services.AddTransient<IOrderDetailDal, EfOrderDetailDal>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession();
            //services.AddDbContext<NorthwindContext>(c => c.UseSqlServer());

            //services.AddDbContext<AppDbContext>(c => c.UseSqlServer());
            services.AddDbContext<AppDbContext>(opts =>
               opts.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = true;

                options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";

            })

                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconfirmation");
          
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
           opt.TokenLifespan = TimeSpan.FromHours(2));

            services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromDays(3));

            services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsFactory>();

            services.AddAutoMapper(typeof(Startup));
    
            var emailConfig = Configuration
             .GetSection("EmailConfiguration")
             .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddControllersWithViews();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
