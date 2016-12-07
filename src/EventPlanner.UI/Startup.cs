using EventPlanner.BL.Configuration;
using EventPlanner.BL.Facades;
using EventPlanner.BL.Facades.Interfaces;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace EventPlanner.UI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.ConfigureBLServices();
            services.AddTransient<IUserFacade, UserFacade>();
            services.AddTransient<IEventFacade, EventFacade>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            BL.Configuration.AutoMapper.Init();

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "Cookie",
                LoginPath = new PathString("/Account/Login"),
                AutomaticChallenge = true,
                AutomaticAuthenticate = true
            });

            app.UseGoogleAuthentication(new GoogleOptions()
            {
                ClientId = Environment.GetEnvironmentVariable("GoogleClientId"),
                ClientSecret = Environment.GetEnvironmentVariable("GoogleSecret"),
                AuthenticationScheme = "Google",
                CallbackPath = "/googleAuthCallback",
                SignInScheme = "Cookie",
                AutomaticAuthenticate = true,
                Scope = { "openid", "profile", "email" },
                Events = new OAuthEvents
                {
                    OnTicketReceived = async context =>
                    {
                        // Ensure user exists
                        var userService = context.HttpContext.RequestServices.GetService<IUserFacade>();
                        await userService.CreateOrGetUser(context.Principal.FindFirst(ClaimTypes.Email).Value);
                    }
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
