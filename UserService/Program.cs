using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using User.Application;
using User.Domain;
using Microsoft.EntityFrameworkCore;
using User.Domain.Repositories; // dla IUserRepository
using System.Security.Cryptography;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserDataContext>(options =>
    options.UseInMemoryDatabase("Users")); // na pocz¹tek in-memory

builder.Services.AddScoped<IUserRepository, UserRepository>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Wpisz token w formacie: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        },
        new List<string>()
    }
});

}); ;
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IResetPasswordService, ResetPasswordService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();


// JWT config
var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtSettings>(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtConfig = jwtSettings.Get<JwtSettings>();
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Administrator"));
}); ;


var app = builder.Build();

// Configure the HTTP request pipeline.
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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserDataContext>();
    db.Users.Add(new User.Domain.Models.UserModel
    {
        Username = "admin",
        Password = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("password"))),
        Role = "Administrator"
    });
    db.SaveChanges();
}
