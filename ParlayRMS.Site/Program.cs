using Fanex.Data;
using Fanex.Logging.Sentry.RavenClient;
using Fanex.NAP.Client.Attributes;
using Fanex.NAP.SignIn;
using Microsoft.AspNetCore.Mvc;
using ONEbook.UM.AspNetCore.Filters;
using ParlayRMS.Site.Utilities;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllersWithViews(options =>
{
    options.Filters.Add(new ServiceFilterAttribute(typeof(CheckUMFilterAttribute)));
    options.Filters.Add(new SessionAuthorizeAttribute());
});
services.AddBundle();
services.AddRazorPages();
services.AddCorsPolicy();
services.AddAuthorizationPolicy();
services.AddAuthentication(configuration);
services.AddRedis(configuration);
services.AddSession(configuration);
services.AddFanexLogSentry(configuration);
services.AddFanex(configuration);
services.AddNapServices();
services.AddNapClient(configuration);
services.AddOneBookUm(configuration);
services.AddFanexData(configuration);
services.AddWinLossRiskControlServices();
services.AddLeagueEventControlServices();
services.AddManageAlertServices();
services.AddLegacyService();

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseWebOptimizer();
app.UseStaticFiles();

app.UseCookiePolicy();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseSession();
app.UseRouting();
app.UseCors("AllowOrigin");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
