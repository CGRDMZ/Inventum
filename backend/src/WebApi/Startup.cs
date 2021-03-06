using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application;
using Domain;
using Application.Commands;
using MediatR.Registration;
using MediatR;
using System.Reflection;
using Application.Services;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.HttpOverrides;
using Npgsql;

namespace WebApi
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT containing userid claim",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                var security =
    new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                UnresolvedReference = true
            },
            new List<string>()
        }
    }; c.AddSecurityRequirement(security);
            });

            services.AddMediatR(typeof(OpenNewBoardCommandHandler));

            var connectionString = Environment.GetEnvironmentVariable("DEPLOY_HEROKU") == "true" ? ParseHerokuDatabaseUrl(Environment.GetEnvironmentVariable("DATABASE_URL")) : Configuration.GetConnectionString("postgres");


            services.AddDbServices<ApplicationDbContext>(connectionString);
            services.AddDbServices<MyIdentityDbContext>(connectionString);


            services.AddHealthChecks();


            services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<MyIdentityDbContext>().AddDefaultTokenProviders();

            services.AddAuthorization().AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
              {
                  o.RequireHttpsMetadata = false;
                  o.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = false,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Jwt:Secret"))),
                      ClockSkew = TimeSpan.Zero,
                      ValidateAudience = false,

                  };
              });

            services.AddCors(o =>
            {
                o.AddDefaultPolicy(p =>
                {
                    p.AllowAnyOrigin();
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                });
            });

            services.AddTransient<IBoardRepository, BoardRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICardRepository, CardRepository>();
            services.AddTransient<IApplicationUserRepository, EfIdentityUserRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICardService, CardService>();

            services.AddTransient<IJwtTokenService, JwtTokenService>();

            services.AddTransient<ICardLocationService, CardLocationService>();
            services.AddMediatR(typeof(OpenNewBoardCommandHandler));
        }

        private string ParseHerokuDatabaseUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri databaseUri))
            {
                throw new ArgumentException("Invalid URL", nameof(url));
            }

            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            return builder.ToString();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (Environment.GetEnvironmentVariable("DEPLOY_HEROKU") == "true")
            {
                // make domain migrations
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    if (ctx.Database.GetPendingMigrations().Any())
                    {
                        ctx.Database.Migrate();
                    }
                }
                // make identity migrations
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<MyIdentityDbContext>();
                    if (ctx.Database.GetPendingMigrations().Any())
                    {
                        ctx.Database.Migrate();
                    }
                }
            }

            app.UsePathBase("/api");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "WebApi v1");
                c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
            });

            app.UseHealthChecks("/health");


            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
