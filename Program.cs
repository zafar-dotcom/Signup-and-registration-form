using Complete.Models;
using Complete.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Segment.Model;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var services = builder.Services;
var configuration = builder.Configuration;
//services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDBContext>().AddDefaultTokenProviders();

services.AddScoped<IExport_To_Excell, Implement___interface_Export_To_Excell>();
//services.AddScoped<IExport_pdf, Implement_interface_Export_pdf>();
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
    };
}
);
services.AddAuthentication().AddGoogle(opt =>
{
    opt.ClientId = configuration["Authentication:Google:ClientId"];
    opt.ClientSecret = configuration["Authentication:Google:ClientSecret"];
    opt.SignInScheme = IdentityConstants.ExternalScheme;
});
services.AddMvc();

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
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
