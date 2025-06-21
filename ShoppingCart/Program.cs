using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Infrastructure.Repositories;
using ShoppingCart.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ShoppingCart.Application.Services.CartService).Assembly));

// Register dependencies (DIP)
builder.Services.AddSingleton<ICartRepository, InMemoryCartRepository>();
builder.Services.AddSingleton<ICartAdder, CartService>();
builder.Services.AddSingleton<ICartRemover, CartService>();
builder.Services.AddSingleton<ICartReader, CartService>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
