using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Computer_Mart.Data;
using System.Security.Claims;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<Computer_MartContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Computer_MartContext") ?? throw new InvalidOperationException("Connection string 'Computer_MartContext' not found.")));


// Add services to the container.
builder.Services.AddAuthentication(Constants.AuthCookieString).AddCookie(Constants.AuthCookieString, options =>
{
	options.Cookie.Name = Constants.AuthCookieString;
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminRequired", policy =>
	{
		policy.RequireClaim(ClaimTypes.Role, "Admin");
	});
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(60 * 24);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

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

app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
