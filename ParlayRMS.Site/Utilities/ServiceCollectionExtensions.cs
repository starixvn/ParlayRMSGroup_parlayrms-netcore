using AutoMapper;
using Fanex.Caching.Redis;
using Fanex.Data.Repository;
using Fanex.Logging;
using Fanex.Logging.Extensions;
using Fanex.Logging.Extensions.AspNetCore;
using Fanex.Logging.Extensions.Database;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Web;
using Fanex.NAP.Client.Services;
using Fanex.NAP.Client.Session;
using Fanex.RemotingRef;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Authorization;
using ParlayRMS.LeagueEventControl;
using ParlayRMS.LeagueEventControl.Excels;
using ParlayRMS.ManageAlert.AutoMapperProfiles;
using ParlayRMS.ManageAlert.Handlers;
using ParlayRMS.ManageAlert.Services;
using ParlayRMS.WinLossRiskControl;
using ParlayRMS.WinLossRiskControl.Handlers;
using ParlayRMS.WinLossRiskControl.Services;
using ParlayRMS.WinLossRiskControl.Shared;
using StackExchange.Redis;
using LeagueEventHandlers = ParlayRMS.LeagueEventControl.Handlers;
using LeagueEventServices = ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.Site.Utilities;

using Fanex.Logging.Sentry.RavenClient;
using Legacy.DataAccess;
using Legacy.Services;
using NUglify.Css;
using NUglify.JavaScript;
using SharpRaven;

public static class ServiceCollectionExtensions
{
    public static readonly string AllowOrigin = "AllowOrigin";

    public static void AddNapServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IGroupService, GroupService>();
        services.AddTransient<ISiteService, SiteService>();
        services.AddTransient<ISecurityService, SecurityService>();
        services.AddTransient<INapSession, NapSession>();
        services.AddTransient<INapSettings, NapSettings>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IDynamicRepository, DynamicRepository>();
        services.AddTransient<Fanex.Logging.ILogger, Logger>();
        services.AddSingleton<IRemotingRefService, RemotingRefService>();

        services.AddSingleton<IMediator, Mediator>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(FilterHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ReportHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ReportExcelHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SiteReportExcelHandler).Assembly));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LeagueEventHandlers.FilterHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LeagueEventHandlers.LeagueEventHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LeagueEventHandlers.EventDetailHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LeagueEventHandlers.EventHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LeagueEventHandlers.ExcelHandler).Assembly));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ManageAlertHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SimilarTicketDetailHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PotentialWinningDetailHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ViewLogHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TakeActionHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ExcelHandler).Assembly));

        services.AddSingleton(CreateMapper);
    }

    public static void AddWinLossRiskControlServices(this IServiceCollection services)
    {
        services.AddTransient<WinLossExcelBuilder, WinLossExcelBuilderImp>();
        services.AddTransient<WinLossService, WinLossServiceImp>();
        services.AddTransient<SiteExcelService, SiteExcelServiceImp>();
        services.AddTransient<WinLossRiskControl.Services.FilterService, WinLossRiskControl.Services.FilterServiceImp>();
    }

    public static void AddLeagueEventControlServices(this IServiceCollection services)
    {
        services.AddTransient<LeagueEventServices.FilterService, LeagueEventServices.FilterServiceImp>();
        services.AddTransient<LeagueEventServices.LeagueEventService, LeagueEventServices.LeagueEventServiceImp>();
        services.AddTransient<ExcelBuilder, ExcelBuilderImp>();
    }

    public static void AddManageAlertServices(this IServiceCollection services)
    {
        services.AddTransient<ManageAlertService, ManageAlertServiceImp>();
        services.AddTransient<ManageAlert.Excels.ExcelBuilder, ManageAlert.Excels.ExcelBuilderImp>();
        services.AddTransient<ManageAlert.Services.FilterService, ManageAlert.Services.FilterServiceImp>();
    }

    public static void AddLegacyService(this IServiceCollection services)
    {
        services.AddTransient<AgentService, AgentService>();
        services.AddTransient<BetDetailsService, BetDetailsService>();
        services.AddTransient<BetListService, BetListService>();
        services.AddTransient<CommonService, CommonService>();
        services.AddTransient<LeagueBetTypeAnalysisService, LeagueBetTypeAnalysisService>();
        services.AddTransient<LeaguesService, LeaguesService>();
        services.AddTransient<LeagueWinningMonitorService, LeagueWinningMonitorService>();
        services.AddTransient<MembersService, MembersService>();
        services.AddTransient<MemberWinLossService, MemberWinLossService>();
        services.AddTransient<ParlayMonitorService, ParlayMonitorService>();
        services.AddTransient<ParlayWonService, ParlayWonService>();
        services.AddTransient<PotentialWinningMonitorService, PotentialWinningMonitorService>();
        services.AddTransient<SimilarBetsService, SimilarBetsService>();
        services.AddTransient<TopWinnersService, TopWinnersService>();
        services.AddTransient<TotalOutstandingService, TotalOutstandingService>();
        services.AddTransient<ViewIPInfoService, ViewIPInfoService>();
        services.AddTransient<WinlossService, WinlossService>();

        services.AddTransient<AgentsDataAccess, AgentsDataAccess>();
        services.AddTransient<BetDetailsDataAccess, BetDetailsDataAccess>();
        services.AddTransient<BetListDataAccess, BetListDataAccess>();
        services.AddTransient<CommonDataAccess, CommonDataAccess>();
        services.AddTransient<LeagueBetTypeAnalysisDataAccess, LeagueBetTypeAnalysisDataAccess>();
        services.AddTransient<LeaguesDataAccess, LeaguesDataAccess>();
        services.AddTransient<LeagueWinningMonitorDataAccess, LeagueWinningMonitorDataAccess>();
        services.AddTransient<MembersDataAccess, MembersDataAccess>();
        services.AddTransient<MemberWinLossDataAccess, MemberWinLossDataAccess>();
        services.AddTransient<ParlayMonitorDataAccess, ParlayMonitorDataAccess>();
        services.AddTransient<ParlayWonDataAccess, ParlayWonDataAccess>();
        services.AddTransient<PotentialWinningMonitorDataAccess, PotentialWinningMonitorDataAccess>();
        services.AddTransient<SimilarBetsDataAccess, SimilarBetsDataAccess>();
        services.AddTransient<TopWinnersDataAccess, TopWinnersDataAccess>();
        services.AddTransient<TotalOutstandingDataAccess, TotalOutstandingDataAccess>();
        services.AddTransient<ViewIPInfoDataAccess, ViewIPInfoDataAccess>();
        services.AddTransient<WinlossDataAccess, WinlossDataAccess>();
    }

    private static IMapper CreateMapper
    {
        get
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new WinLossMappingProfile());
                mc.AddProfile(new LeagueEventMappingProfile());
                mc.AddProfile(new ManageAlertMappingProfile());
            });

            return mapperConfig.CreateMapper();
        }
    }

    public static void AddFanex(this IServiceCollection services, IConfiguration configuration)
    {
        LogManager
            .SetDefaultLogCategory(configuration.GetSection("LogCategory").Value)
            .Use(new List<ILogEngine>
            {
                new SentryLogging(new SentryEngineOptions
                {
                    Dsn = new Dsn(configuration.GetSection("SentryEngineOptions:Dsn").Value)
                })
            });

        RefDataTypeContainer
            .ConnectTo(new Uri(configuration.GetSection("RemotingRef").Value))
            .FromApplication(configuration.GetSection("LogCategory").Value)
            .SupportOnlyDefaultLanguage();

        ExceptionHandler
            .UseConfig(new LoggingConfiguration())
            .UseGetDbExceptionInfo(DatabaseInfoGetters.GetDbExceptionInfo);
    }

    public static IServiceCollection AddBundle(this IServiceCollection services)
    {
        services.AddWebOptimizer(pipeline =>
        {
            var cssSetting = new CssSettings { OutputMode = NUglify.OutputMode.MultipleLines, CommentMode = CssComment.None };
            var scriptSetting = new CodeSettings { MinifyCode = false };
            pipeline.AddLayoutBundle(cssSetting, scriptSetting);
        });

        return services;
    }

    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = configuration.GetSection("AuthSettings:LoginPath").Value;
                options.LogoutPath = configuration.GetSection("AuthSettings:LogoutPath").Value;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(int.Parse(configuration.GetSection("AuthSettings:CookieTimeOut").Value));
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

    public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConfig = configuration.GetSection("AuthSettings:RedisConfig").Value;
        var redis = ConnectionMultiplexer.Connect(redisConfig);

        services.AddDataProtection()
            .PersistKeysToStackExchangeRedis(redis, "Fanex.ParlayRMS-ProtectionKeys");

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfig;
            options.InstanceName = "Fanex.ParlayRMS.Session-";
        });

        services.AddRedisCache(configuration.GetSection("fanex.caching.redis"));
    }

    public static void AddSession(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSession(options =>
        {
            options.Cookie.IsEssential = true;
            options.IdleTimeout = TimeSpan.FromMinutes(int.Parse(configuration.GetSection("AuthSettings:SessionTimeOut").Value));
            options.Cookie.Name = "Fanex.ParlayRMS.Session";
        });
    }

    public static void AddAuthorizationPolicy(this IServiceCollection services)
    {
        services.AddControllers(config =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            config.Filters.Add(new AuthorizeFilter(policy));
        });
    }

    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AllowOrigin,
                builder =>
                {
                    builder.WithOrigins("http://localhost:57420", "http://localhost:57421", "http://localhost:61034",
                            "https://prms.nexdev.net", "https://prms-core.nexdev.net",
                            "https://napparlayrms.starixuat.com", "https://prmscore.starixuat.com",
                            "https://parlayrms.oneops.net", "https://prmscore.oneops.net",
                            "https://parlayrms.sportsfun.app", "https://prmscore.sportsfun.app",
                            "http://parlayrms.byb6181.com", "https://parlayrms.byb6181.com")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
        });
    }
}