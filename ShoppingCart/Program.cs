using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Infrastructure.Repositories;
using ShoppingCart.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ShoppingCart.Application.Services.CartService).Assembly));

// Register dependencies (DIP)
// Rejestracja zale¿noœci dla ró¿nych us³ug
builder.Services.AddScoped<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddSingleton<ICartRepository, InMemoryCartRepository>();
builder.Services.AddSingleton<ICartAdder, CartService>(); // Mo¿e byæ oddzielna klasa CartAdderService
builder.Services.AddSingleton<ICartRemover, CartService>(); // Mo¿e byæ oddzielna klasa CartRemoverService
builder.Services.AddSingleton<ICartReader, CartService>(); // Mo¿e byæ oddzielna klasa CartReaderService

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

