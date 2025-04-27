global using assnet8.Data;
global using assnet8.Dtos.Simple;
global using assnet8.Identity;
global using assnet8.Models;
global using assnet8.Utils;

global using Microsoft.EntityFrameworkCore;

using System.Text;
using System.Text.Json;

using assnet8.Middleware;
using assnet8.Services.Account;
using assnet8.Services.Auth;
using assnet8.Services.Entries;
using assnet8.Services.Images;
using assnet8.Services.SignalR;
using assnet8.Services.Utils;
using assnet8.Swagger;

using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// mislim da mu ovo omogucava dependency injection(i mislim da mozda omogucava i koriscenje tog middlewerea)


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // 🔥 required for sending cookies/auth
    });
});


builder.Services.AddAuthentication(configureOptions =>
{
    configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //TODO dodaj ovo nakon sto dodas linkove za front i back u zavisnosti dal je production ili development, ovo ce biti pred kraj aplikacije
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:ACCESS_TOKEN_SECRET"]!))
    };


    // Important: allow SignalR JWTs in query string
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/entries"))
            {
                // Read the token from the query string
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();// ovo mi omogucava pristup contextu u servisima (u kontrolerima automatcki postoje)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("DefaultConnection")));



builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>(); //filter je micro middleware
})
    .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddHttpClient<IGoogleMapsService, GoogleMapsService>(client =>
{
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("NextJsRevalidation", client =>
{
    client.BaseAddress = new Uri(config["Frontend:Url"]!);
    client.Timeout = TimeSpan.FromSeconds(30);
});
builder.Services.AddSingleton<INextJsRevalidationService, NextJsRevalidationService>();
builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
//AddScoped -> create new instance for every request
//AddTransient -> new instance for every controller and every service for every request
//AddSingleton -> only one instance for every request
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// middleware posle ovoga

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.MapHub<EntriesHub>("/hubs/entries");

// poslednji middleware
app.MapControllers();

app.Run();