using AuctionHouse.Data;
using AuctionHouse.Models;
using AuctionHouse.Services.CustomAuthorization;
using AuctionHouse.Services.ItemService;
using AuctionHouse.Services.OrderService;
using AuctionHouse.Services.UserService;
using AuctionHouse.Services.AzureStorageService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using AuctionHouse.DAO.ItemDAO;
using AuctionHouse.DAOs.UserDAO;
using AuctionHouse.DAOs.OrderDAO;
using Hangfire;
using Hangfire.PostgreSql;
using AuctionHouse.Services.ServiceManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDatabase")));
builder.Services.AddHangfire(config => config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("PostgresDatabase")));

builder.Services.AddHangfireServer();

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAzureStorageRepository, AzureStorageRepository>();
builder.Services.AddScoped<IServiceManagement, ServiceManagement>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "ASP.NET";
    options.IdleTimeout = TimeSpan.FromDays(7); // da go pomislq 
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IAuthorizationHandler, SessionBasedAuthorizationHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.Requirements.Add(new SessionBasedAuthorizationRequirement(Role.Admin)));
    options.AddPolicy("User", policy => policy.Requirements.Add(new SessionBasedAuthorizationRequirement(Role.User)));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

RecurringJob.AddOrUpdate<IServiceManagement>(serviceManagement => serviceManagement.CheckForExpiredAuctions(), Cron.Daily);

app.Run();
