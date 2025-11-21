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

builder.Services.AddTransient<ItemService>(p =>
{
    var itemRepository = p.GetRequiredService<IItemRepository>();
    return new ItemService(itemRepository);
});

builder.Services.AddTransient<IItemRepository>(_ =>
{
    return new ItemRepository(dbConnString);
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