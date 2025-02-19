global using Microsoft.EntityFrameworkCore;
global using assnet8.Models;
global using assnet8.Data;
global using assnet8.Identity;
global using assnet8.Dtos.Simple;
global using assnet8.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using assnet8.Swagger;
using System.Text.Json;
using FluentValidation;
using assnet8.Middleware;
using assnet8.Services.Auth;
using assnet8.Services.Account;
using assnet8.Services.Entries;
using assnet8.Services.Images;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// mislim da mu ovo omogucava dependency injection(i mislim da mozda omogucava i koriscenje tog middlewerea)

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

app.UseAuthentication();
app.UseAuthorization();

// poslednji middleware
app.MapControllers();

app.Run();

