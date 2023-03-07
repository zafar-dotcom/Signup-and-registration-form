using Complete.CustomHandler;
using Complete.DAL_Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var services = builder.Services;
var Configuration = builder.Configuration;
services.AddScoped<IUser_Login_Registraion, User_implement>();
services.AddScoped<ICustomer, Customer_implement>();
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options => {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = Configuration["Jwt:Issuer"],
                       ValidAudience = Configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                   };
               });
services.AddAuthentication("CookieAuthentication")
                .AddCookie("CookieAuthentication", config =>
                {
                    config.Cookie.Name = "UserLoginCookie";
                    config.LoginPath = "/Login/UserLogin";
                    config.AccessDeniedPath = "/Login/UserAccessDenied";
                });
/*services.AddAuthorization(options =>
{
    var userauthpolicybuilder = new AuthorizationPolicyBuilder();
    options.DefaultPolicy = userauthpolicybuilder
                            .RequireAuthenticatedUser()
                            .RequireClaim(ClaimTypes.DateOfBirth)
                            .Build();

});*/
services.AddAuthorization(opt =>
{
    opt.AddPolicy("UserPolicy", policybulider =>
    {
        policybulider.UserRequireCustomClaim(ClaimTypes.Email);
        policybulider.UserRequireCustomClaim(ClaimTypes.DateOfBirth);
    });


});
services.AddScoped<IAuthorizationHandler, PoliciesAuthorizationHandler>();
services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();



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
