using AutoMapper;
using Fanex.Caching.Redis;
using Fanex.Data;
using Fanex.Data.Repository;
using Fanex.Data.SqlServer;
using Fanex.Logging;
using Fanex.Logging.Extensions;
using Fanex.Logging.Extensions.AspNetCore;
using Fanex.Logging.Extensions.Database;
using Fanex.Logging.Sentry;
using Fanex.RemotingRef;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParlayRMS.Auth.Services;
using ParlayRMS.WinLossRiskControl;
using ParlayRMS.WinLossRiskControl.Handlers;
using ParlayRMS.WinLossRiskControl.Services;
using ParlayRMS.WinLossRiskControl.Shared;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ParlayRMS.Site
{
    public partial class Startup
    {
        private static readonly string allowOrigin = "AllowOrigin";

        private static void ConfigureNapServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IDynamicRepository, DynamicRepository>();
            services.AddTransient<ILogger, Logger>();
            services.AddSingleton<IRemotingRefService, RemotingRefService>();

            services.AddSingleton<IMediator, Mediator>();
            services.AddMediatR(typeof(FilterHandler).Assembly);
            services.AddMediatR(typeof(ReportHandler).Assembly);
            services.AddMediatR(typeof(ReportExcelHandler).Assembly);
            services.AddMediatR(typeof(SiteReportExcelHandler).Assembly);

            services.AddSingleton(CreateMapper);
        }

        private static void ConfigureWinLossRiskControlServices(IServiceCollection services)
        {
            services.AddTransient<WinLossExcelBuilder, WinLossExcelBuilderImp>();
            services.AddTransient<WinLossService, WinLossServiceImp>();
            services.AddTransient<SiteExcelService, SiteExcelServiceImp>();
            services.AddTransient<FilterService, FilterServiceImp>();
            services.AddTransient<UserLoginService, UserLoginServiceImp>();
        }

        private static IMapper CreateMapper
        {
            get
            {
                var mapperConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new WinLossMappingProfile());
                });

                return mapperConfig.CreateMapper();
            }
        }

        private void ConfigureFanex()
        {
            var connections = Configuration.GetSection("ConnectionStrings")
                                   .GetChildren()
                                   .AsEnumerable()
                                   .ToDictionary(
                                            x => x.GetValue<string>("Name"),
                                            y => new ConnectionConfiguration(y.GetValue<string>("Name"), y.GetValue<string>("ConnectionString"), y.GetValue<string>("ProviderName"))
                                    );

            DbSettingProviderManager
              .StartNewSession()
              .Use(connections)
              .WithSqlServer()
              .UseDefaultDbSettingProvider(resourcePath: "~/App_Data")
              .Run();

            LogManager
            .SetDefaultLogCategory(Configuration.GetSection("LogCategory").Value)
            .Use(new List<ILogEngine>
            {
                new SentryLogging(new SentryEngineOptions
                {
                    Dsn = new Dsn(Configuration.GetSection("SentryEngineOptions:Dsn").Value)
                })
            });

            RefDataTypeContainer
            .ConnectTo(new Uri(Configuration.GetSection("RemotingRef").Value))
            .FromApplication(Configuration.GetSection("LogCategory").Value)
            .SupportOnlyDefaultLanguage();

            ExceptionHandler
                .UseConfig(new LoggingConfiguration())
                .UseGetDbExceptionInfo(DatabaseInfoGetters.GetDbExceptionInfo);
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                        .AddCookie(options =>
                        {
                            options.LoginPath = Configuration.GetSection("AuthSettings:LoginPath").Value;
                            options.LogoutPath = Configuration.GetSection("AuthSettings:LogoutPath").Value;
                            options.ExpireTimeSpan = TimeSpan.FromMinutes(int.Parse(Configuration.GetSection("AuthSettings:CookieTimeOut").Value));
                            options.Cookie = new CookieBuilder
                            {
                                Name = "Fanex.ParlayRMS.Cookie",
                                SecurePolicy = CookieSecurePolicy.SameAsRequest
                            };
                            options.Events.OnSigningOut = context =>
                            {
                                context.Response.Redirect(options.LoginPath);
                                return Task.CompletedTask;
                            };
                        });
        }

        private void ConfigureRedis(IServiceCollection services)
        {
            var redisConfig = Configuration.GetSection("AuthSettings:RedisConfig").Value;
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConfig;
                options.InstanceName = $"Fanex.ParlayRMS.Session-";
            });

            services.AddRedisCache(Configuration.GetSection("fanex.caching.redis"));
        }

        private void ConfigureSession(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(int.Parse(Configuration.GetSection("AuthSettings:SessionTimeOut").Value));
                options.Cookie.Name = "Fanex.ParlayRMS.Session";
            });
        }

        private static void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        private static void ConfigureCORS(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(allowOrigin,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:57420", "http://localhost:61034",
                                            "https://prms-netcore-v1.nexdev.net",
                                            "https://prms-netcore-v2.nexdev.net",
                                            "https://prms-netcore-v3.nexdev.net",
                                            "https://ha.nexdev.net:7301")
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials();
                    });
            });
        }
    }
}