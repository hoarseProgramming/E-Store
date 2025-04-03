using EStore.Api.Database;
using EStore.Api.Endpoints;
using EStore.Api.Models;
using EStore.Api.Repositories;
using EStore.Api.Services;
using EStore.Api.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("ADMIN"));
});

builder.Services.AddIdentityCore<AuthUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EStoreContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityApiEndpoints<AuthUser>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontendPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7189");
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
        policy.AllowCredentials();
    });
});

builder.Services.AddDbContext<EStoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EStore"));
});

var app = builder.Build();

app.UseCors("frontendPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/api/user")
    .CustomMapIdentityApi<AuthUser>();

app.MapControllers();

app.Run();