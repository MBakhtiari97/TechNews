using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using TechNews.Core.Services;
using TechNews.Core.Services.Interfaces;
using TechNews.DataLayer.Context;
using TechNews.Utility;

namespace TechNews.Web
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
            #region ToastNotification

            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });

            #endregion

            services.AddControllersWithViews();

            Configuration["Address"] = "https://localhost:5001/";
            
            #region TechNewsContext

            services.AddDbContext<TechNewsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TechNews"),
                    b => b.MigrationsAssembly("TechNews.Web"));
            });

            #endregion

            #region IoC

            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<ISupportRepository, SupportRepository>();
            services.AddScoped<INewsletterRepository, NewsletterRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<IBrowserRepository, BrowserRepository>();
            #endregion

            #region Authentication

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/SignIn";
                options.LogoutPath = "/SignOut";
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });

            #endregion
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

           

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                   ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                   ForwardedHeaders.XForwardedProto
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/Author"))
                {
                    if (!context.User.Identity.IsAuthenticated || int.Parse(context.User.FindFirstValue("IsAdmin").ToString()) == 1)
                    {
                        context.Response.Redirect("/SignIn");
                    }
                }

                await next.Invoke();
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/Admin"))
                {
                    if (!context.User.Identity.IsAuthenticated || int.Parse(context.User.FindFirstValue("IsAdmin").ToString())!=3)
                    {
                        context.Response.Redirect("/SignIn");
                    }
                }

                await next.Invoke();
            });

            app.UseNotyf();

           
            var result = Configuration["Address"];

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
