global using Microsoft.EntityFrameworkCore;
global using assnet8.Models;
global using assnet8.Data;

var builder = WebApplication.CreateBuilder(args);

// mislim da mu ovo omogucava dependency injection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// mislim da je ovo middleware
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

