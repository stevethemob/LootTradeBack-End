using LootTradeInterfaces;
using LootTradeServices;
using LootTradeRepositories;
using LootTradeServices.validators;
using LootTradeDomainModels;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string dbConnString = builder.Configuration.GetConnectionString("conn");

var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection["Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException("Jwt:Key is missing from configuration.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            )
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<User>, UserValidator>();

builder.Services.AddTransient<UserService>(p =>
{
    var userRepository = p.GetRequiredService<IUserRepository>();
    var userValidator = p.GetRequiredService<IValidator<User>>();
    return new UserService(userRepository, userValidator);
});

builder.Services.AddTransient<IUserRepository>(_ =>
    new UserRepository(dbConnString));

builder.Services.AddTransient<GameService>(p =>
    new GameService(p.GetRequiredService<IGameRepository>()));

builder.Services.AddTransient<IGameRepository>(_ =>
    new GameRepository(dbConnString));

builder.Services.AddTransient<ItemService>(p =>
    new ItemService(p.GetRequiredService<IItemRepository>()));

builder.Services.AddTransient<IItemRepository>(_ =>
    new ItemRepository(dbConnString));

builder.Services.AddTransient<InventoryService>(p =>
    new InventoryService(p.GetRequiredService<IInventoryRepository>()));

builder.Services.AddTransient<IInventoryRepository>(_ =>
    new InventoryRepository(dbConnString));

builder.Services.AddSingleton<JwtService>();

var app = builder.Build();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
