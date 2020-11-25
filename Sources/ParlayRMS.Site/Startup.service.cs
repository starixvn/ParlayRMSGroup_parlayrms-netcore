using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
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
using ParlayRMS.WinLossRiskControl;
using ParlayRMS.WinLossRiskControl.Handlers;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Services;
using Sentry;

namespace ParlayRMS.Site
{
    public partial class Startup
    {
        private void ConfigureNapServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IDynamicRepository, DynamicRepository>();
            services.AddTransient<ILogger, Logger>();
            services.AddSingleton<IRemotingRefService, RemotingRefService>();

            services.AddSingleton<IMediator, Mediator>();
            services.AddMediatR(typeof(FilterHandler).Assembly);
            services.AddMediatR(typeof(ReportHandler).Assembly);
            services.AddMediatR(typeof(ReportExcelHandler).Assembly);
           
            services.AddSingleton(CreateMapper);

            services.AddSingleton<WinLossService, WinLossServiceImp>();
             services.AddSingleton<WinLossExcelService, WinLossExcelServiceImp>();
            services.AddSingleton<FilterService, FilterServiceImp>();
        }

        private IMapper CreateMapper
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

        private void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }
    }
}