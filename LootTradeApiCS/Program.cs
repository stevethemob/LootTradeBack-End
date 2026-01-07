using LootTradeInterfaces;
using LootTradeServices;
using LootTradeRepositories;
using LootTradeServices.validators;
using LootTradeDomainModels;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LootTradeApiCSJwtService;

var builder = WebApplication.CreateBuilder(args);

string dbConnString = builder.Configuration.GetConnectionString("conn")
    ?? throw new InvalidOperationException("Connection string 'conn' is missing.");

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
{
    return new UserRepository(dbConnString);
});

builder.Services.AddTransient<GameService>(p =>
{ 
    var gameRepository = p.GetRequiredService<IGameRepository>();
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

builder.Services.AddTransient<OfferService>(p =>
{
    var offerRepository = p.GetRequiredService<IOfferRepository>();
    var inventoryRepository = p.GetRequiredService<IInventoryRepository>();
    return new OfferService(offerRepository, inventoryRepository);
});

builder.Services.AddTransient<IOfferRepository>(_ =>
{
    return new OfferRepository(dbConnString);
});

builder.Services.AddTransient<TradeService>(p =>
{
    var tradeRepsitory = p.GetRequiredService<ITradeRepository>();
    return new TradeService(tradeRepsitory);
});

builder.Services.AddTransient<ITradeRepository>(_ =>
{
    return new TradeRepository(dbConnString);
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

await app.RunAsync();
