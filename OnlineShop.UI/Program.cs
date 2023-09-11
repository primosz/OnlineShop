using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Database;
using OnlineShop.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("OnlineShop.Database")));
builder.Services.AddControllers();
builder.Services.AddTransient<IBraintreeService, BraintreeService>();
builder.Services.AddSession(options =>
{
	options.Cookie.Name = "Cart";
	options.Cookie.MaxAge = TimeSpan.FromDays(365);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(name: "default", pattern: "{controller=Admin}/{action=Index}/{id?}"); });
app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });

app.MapRazorPages();
app.UseCookiePolicy();

app.Run();

