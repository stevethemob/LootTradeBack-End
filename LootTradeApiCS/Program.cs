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

// 🔹 Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("Jwt");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt["Key"])
            )
        };
    });

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
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
    var userRepository = p.GetService<IUserRepository>();
    var userValidator = p.GetService<IValidator<User>>();
    return new UserService(userRepository, userValidator);
});

builder.Services.AddTransient<IUserRepository>(_ =>
{
    return new UserRepository(dbConnString);
});

builder.Services.AddTransient<GameService>(p =>
{
    var gameRepository = p.GetService<IGameRepository>();
    return new GameService(gameRepository);
});

builder.Services.AddTransient<IGameRepository>(_ =>
{
    return new GameRepository(dbConnString);
});

builder.Services.AddTransient<ItemService>(p =>
{
    var itemRepository = p.GetRequiredService<IItemRepository>();
    return new ItemService(itemRepository);
});

builder.Services.AddTransient<IItemRepository>(_ =>
{
    return new ItemRepository(dbConnString);
});

builder.Services.AddTransient<InventoryService>(p =>
{
    var inventoryRepository = p.GetRequiredService<IInventoryRepository>();
    return new InventoryService(inventoryRepository);
});

builder.Services.AddTransient<IInventoryRepository>(_ =>
{
    return new InventoryRepository(dbConnString);
});

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
