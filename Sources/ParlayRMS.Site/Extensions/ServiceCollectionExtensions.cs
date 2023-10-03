using AutoMapper;
using Fanex.Caching.Redis;
using Fanex.Data;
using Fanex.Data.Repository;
using Fanex.Data.SqlServer;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeagueEventHandlers = ParlayRMS.LeagueEventControl.Handlers;
using LeagueEventServices = ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.Site.Extensions;

using Fanex.Logging.Sentry.RavenClient;
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
		services.AddTransient<ILogger, Logger>();
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
		var connections = configuration.GetSection("ConnectionStrings")
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
			.UseDefaultDbSettingProvider(resourcePath: configuration.GetSection("StoredProcedurePath").Value)
			.Run();

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

	public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
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

	public static void AddCustomSession(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSession(options =>
		{
			options.Cookie.IsEssential = true;
			options.IdleTimeout = TimeSpan.FromMinutes(int.Parse(configuration.GetSection("AuthSettings:SessionTimeOut").Value));
			options.Cookie.Name = "Fanex.ParlayRMS.Session";
		});
	}

	public static void AddCustomAuthorization(this IServiceCollection services)
	{
		services.AddControllers(config =>
		{
			var policy = new AuthorizationPolicyBuilder()
				.RequireAuthenticatedUser()
				.Build();
			config.Filters.Add(new AuthorizeFilter(policy));
		});
	}

	public static void AddCustomCors(this IServiceCollection services)
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
							"https://parlayrms.sportsfun.app", "https://prmscore.sportsfun.app")
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials();
				});
		});
	}
}