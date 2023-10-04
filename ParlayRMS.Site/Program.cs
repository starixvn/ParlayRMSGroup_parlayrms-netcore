using Fanex.NAP.Client;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Web;
using ParlayRMS.Site;
using ParlayRMS.Site.CrossCuttingConcern;
using ParlayRMS.Site.Extensions;
using Fanex.Data.SqlServer;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddCustomCors();
services.AddCustomAuthorization();
services.AddCustomAuthentication(configuration);
services.AddRedis(configuration);
services.AddCustomSession(configuration);
services.AddFanex(configuration);
services.AddNapServices();
services.AddWinLossRiskControlServices();
services.AddLeagueEventControlServices();
services.AddManageAlertServices();
services.AddOnebookUM(configuration);
services.AddFanexSql(configuration);

services.AddRazorPages();
services.AddControllersWithViews();

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
