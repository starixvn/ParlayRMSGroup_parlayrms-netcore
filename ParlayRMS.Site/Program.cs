using Fanex.NAP.Client;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Web;
using ParlayRMS.Site;
using Fanex.Data.SqlServer;
using ParlayRMS.Site.Utilities;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddCorsPolicy();
services.AddAuthorizationPolicy();
services.AddAuthentication(configuration);
services.AddRedis(configuration);
services.AddSession(configuration);
services.AddFanex(configuration);
services.AddNapServices();
services.AddWinLossRiskControlServices();
services.AddLeagueEventControlServices();
services.AddManageAlertServices();
services.AddOnebookUM(configuration);
services.AddFanexSql(configuration);
services.AddBundle();
services.AddRazorPages();
services.AddControllersWithViews();

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

#if DEBUG
app.Run();
#else
await app.RunAsync("http://0.0.0.0:5000");
#endif
