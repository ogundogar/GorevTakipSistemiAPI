using GorevTakipSistemiAPI.Contexts;
using GorevTakipSistemiAPI.Entities;
using GorevTakipSistemiAPI.Interface.IRepositories.IGorev;
using GorevTakipSistemiAPI.Repositories.Gorev;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GorevTakipDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddIdentity<Kullanici,Role>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<GorevTakipDbContext>();

builder.Services.AddScoped<IRepositoryGorev, RepositoryGorev>();

builder.Services.AddAuthentication("Admin")
    .AddJwtBearer(options => { options.TokenValidationParameters = new() 
        {
            //ValidateAudience = true,
            //ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
        }; 
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
