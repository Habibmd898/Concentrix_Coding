using Concentrix_Coding_Task.RabbitMQ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Concentrix_Producer.Services.APIManager;
using Concentrix_Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDbContext<OrderDbContext>(options =>
//    options.UseInMemoryDatabase("ASPNETCoreRabbitMQ"));
//builder.Services.AddSingleton<IOrderDbContext, OrderDbContext>();
builder.Services.AddCommonService();
builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();
builder.Services.AddScoped<IOrdersAPI, OrderAPI>();
builder.Services.AddMemoryCache();
//builder.Services.AddSingleton<IOrderRepository, OrderRepository>();

builder.Services.AddControllers();
builder.Services.AddAuthentication().AddJwtBearer();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

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
