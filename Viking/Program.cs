
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Viking.Interfaces;
using Viking.Models;
using Viking.Models.Contexts;
using Viking.Models.JWTModels;
using Viking.Models.Sports;
using Viking.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7116",
                    "https://localhost:44490/",
                    "http://localhost:5147")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin 
                .AllowCredentials();
        });
});

// Add services to the container.

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var services = builder.Services;

var secretKey = builder.Configuration.GetSection("JWTSettings:SecretKey").Value;
var issuer = builder.Configuration.GetSection("JWTSettings:Issuer").Value;
var audience = builder.Configuration.GetSection("JWTSettings:Audience").Value;

var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

var connectionStringUsers = builder.Configuration.GetConnectionString("IdentityDB");
var conStringVikingSports = builder.Configuration.GetConnectionString("Viking_Sports");

RegisterDbContexts(builder);

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));

builder.Services
    .ConfigureApplicationCookie(options =>
    {
        // Cookie settings
        options.Cookie.HttpOnly = false;
        options.ExpireTimeSpan = TimeSpan.FromDays(1);

    })
    .AddAuthentication(t =>
    {
        t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        t.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).
    AddJwtBearer(t =>
    {
        t.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            IssuerSigningKey = signInKey,
            ValidateIssuerSigningKey = true,

            ClockSkew = TimeSpan.FromMinutes(1)

        };
        t.Events = new JwtBearerEvents();
        t.Events.OnChallenge = context =>
        {
            // Skip the default logic.
            context.HandleResponse();

            var payload = new JObject
            {
                ["error"] = context.Error,
                ["error_description"] = context.ErrorDescription,
                ["error_uri"] = context.ErrorUri
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 405;

            return context.Response.WriteAsync(payload.ToString());
        };
        //t.Events = new JwtBearerEvents()
        //{
        //    OnAuthenticationFailed = context =>
        //    {
        //        var err = "An error occurred processing your authentication.";
        //        var result = JsonConvert.SerializeObject(new { err });
        //        return context.Response.WriteAsync(result);
        //    }
        //};
    }).AddCookie(options =>
    {
        options.Cookie.Expiration = TimeSpan.FromDays(1);
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });

ReposRegister(services);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(myAllowSpecificOrigins);
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}");

app.MapControllers();

app.MapFallbackToFile("index.html"); ;

app.Run();


void RegisterDbContexts(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<UserDbContext>(options => options.UseNpgsql(connectionStringUsers));
    builder.Services.AddDbContext<conViking_Sports>(options => options.UseNpgsql(conStringVikingSports));
    builder.Services.AddDbContext<conViking>(options => options.UseNpgsql(connectionStringUsers));
    builder.Services.AddIdentity<IdentityUser, IdentityRole>(t => { t.Password.RequireNonAlphanumeric = false; }).AddEntityFrameworkStores<UserDbContext>();
}

void ReposRegister(IServiceCollection services)
{
    services.AddScoped<ISet,RSet>();
    services.AddScoped<IExercise,RExercise>();
    services.AddScoped<IWorkout,RWorkout>();
    services.AddScoped<IUserRefreshTokens, RUserRefreshTokens>();
    services.AddScoped<ITokenService, TokenServices>();
}