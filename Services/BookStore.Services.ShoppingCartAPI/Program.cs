using AutoMapper;
using BookStore.Services.ShoppingCartAPI;
using BookStore.Services.ShoppingCartAPI.Data;
using BookStore.Services.ShoppingCartAPI.Extension;
using BookStore.Services.ShoppingCartAPI.Service;
using BookStore.Services.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//SeriLog Setttings
Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File("log/BookStoreShopp�ngCartApi.txt", rollingInterval: RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();
////////////////////////


builder.Services.AddDbContext<UygulamaDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
});

//For AutoMapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//life time config
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

//httpCleint
builder.Services.AddHttpClient("Book", x => x.BaseAddress = new Uri(builder.Configuration["ServiceUrls:BookAPI"]));

builder.Services.AddHttpClient("Discount", x => x.BaseAddress = new Uri(builder.Configuration["ServiceUrls:DiscountAPI"]));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});

builder.AddAppAuthetication();

builder.Services.AddAuthorization();



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
