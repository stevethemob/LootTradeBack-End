using LootTradeInterfaces;
using LootTradeServices;
using LootTradeRepositories;

var builder = WebApplication.CreateBuilder(args);

string dbConnString = builder.Configuration.GetConnectionString("conn");

// Add services to the container.

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<UserService>(p =>
{
    var userRepository = p.GetService<IUserRepository>();
    return new UserService(userRepository);
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

var app = builder.Build();

app.UseCors("AllowFrontend");

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