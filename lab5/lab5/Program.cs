using lab5.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Okta.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseUrls("https://*:1337");
builder.Services.Configure<OktaTokenSettings>(builder.Configuration.GetSection("Okta"));

var oktaSettings = builder.Configuration.GetSection("Okta").Get<OktaTokenSettings>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
})
.AddAuthentication(authOptions =>
{
    authOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOktaMvc(new OktaMvcOptions
{
    AuthorizationServerId = oktaSettings.AuthorizationServerId,
    ClientId = oktaSettings.ClientId,
    OktaDomain = oktaSettings.Domain,
    ClientSecret = oktaSettings.ClientSecret,
    Scope = new List<string> { "openid", "profile", "email", "phone" },
});

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
