using Fanex.NAP.Client;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Web;
using ParlayRMS.Site;
using Fanex.Data.SqlServer;
using ParlayRMS.Site.Utilities;
using Fanex.NAP.SignIn.Attribute;
using Microsoft.AspNetCore.Mvc;
using ONEbook.UM.AspNetCore.Filters;

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
services.AddFanex(configuration);
services.AddNapServices();
services.AddOneBookUm(configuration);
services.AddFanexSql(configuration);
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
NapClientManager
	.UseConfig(app.Services.GetService<INapSettings>())
	.UseSession(app.Services.GetService<INapSession>());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
