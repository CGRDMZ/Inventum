using System;
using Application.Commands;
using Application.Interfaces;
using Application.Services;
using Domain;
using Infrastructure;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcApp
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
            services.AddControllersWithViews();
            services.AddMediatR(typeof(OpenNewBoardCommandHandler));


            services.AddDbServices<ApplicationDbContext>(Configuration.GetConnectionString("Postgres"));
            services.AddDbServices<MyIdentityDbContext>(Configuration.GetConnectionString("Postgres"));

            services.AddIdentity<AppUser, IdentityRole<Guid>>().AddEntityFrameworkStores<MyIdentityDbContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddTransient<IBoardRepository, BoardRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IApplicationUserRepository, EfIdentityUserRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICardService, CardService>();

            services.AddTransient<ICardLocationService, CardLocationService>();

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



            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "swapcard",
                    pattern: "{controller=Board}/{boardId}/CardGroup/{cardGroupId}/{action=SwapCards}");
                endpoints.MapControllerRoute(
                    name: "reposition",
                    pattern: "{controller=Board}/{boardId}/CardGroup/{cardGroupId}/{action=RepositionCards}");
                endpoints.MapControllerRoute(
                    name: "transfer",
                    pattern: "{controller=Board}/{boardId}/CardGroup/{cardGroupId}/{action=TransferCard}");
                endpoints.MapControllerRoute(
                    name: "remove",
                    pattern: "{controller=Board}/{boardId}/CardGroup/{cardGroupId}/{action=RemoveCard}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "sendInvitation",
                    pattern: "{controller=Board}/{boardId}/{action=InviteUser}");
                endpoints.MapControllerRoute(
                    name: "acceptInvitation",
                    pattern: "{controller=Board}/AcceptInvite/{invitationId}/{action=AcceptInvitation}");
            });
        }
    }
}
