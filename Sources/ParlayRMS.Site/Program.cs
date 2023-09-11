using Fanex.NAP.Client;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ParlayRMS.Site;
using ParlayRMS.Site.CrossCuttingConcern;
using ParlayRMS.Site.Extensions;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomCors();
builder.Services.AddCustomAuthorization();
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);
builder.Services.AddCustomSession(builder.Configuration);
builder.Services.AddFanex(builder.Configuration);
builder.Services.AddNapServices();
builder.Services.AddWinLossRiskControlServices();
builder.Services.AddLeagueEventControlServices();
builder.Services.AddManageAlertServices();
OnebookUMInitializer.RegisterServiceCollection(builder.Services, builder.Configuration);
OnebookUMInitializer.InitializeUMClientManager();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseMiddleware<ErrorHandler>();
app.UseSession();
app.UseRouting();
app.UseCors("AllowOrigin");
NapClientManager
	.UseConfig(app.Services.GetService<INapSettings>())
	.UseSession(app.Services.GetService<INapSession>());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

#if DEBUG
app.Run();
#else
await app.RunAsync("http://0.0.0.0:5000");
#endif
