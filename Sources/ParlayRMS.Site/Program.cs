using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ParlayRMS.Site.CrossCuttingConcern;
using ParlayRMS.Site.Extensions;

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

#if DEBUG
app.Run();
#else
await app.RunAsync("http://0.0.0.0:5000");
#endif
