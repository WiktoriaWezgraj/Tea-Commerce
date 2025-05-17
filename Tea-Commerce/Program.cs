using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Tea.Application;
using Tea.Domain.Repositories;
using Tea.Domain.Seeders;

var builder = WebApplication.CreateBuilder(args);
//Server=localhost,1444;Database=TeaShopDb;User Id=sa;Password=MyPass123$;Encrypt=False;TrustServerCertificate=True;

var connectionString = "Server=localhost,1444;Database=TeaShopDb;User Id=sa;Password=MyPass123$;Encrypt=False;TrustServerCertificate=True;";

builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Transient);

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
// Add services to the container.
builder.Services.AddScoped<ICreditCardService, CreditCardService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITeaSeeder, TeaSeeder>();
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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    await db.Database.MigrateAsync();
    var seeder = scope.ServiceProvider.GetRequiredService<ITeaSeeder>();
    await seeder.Seed();
}

app.Run();

public partial class Program { }
