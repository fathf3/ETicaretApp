using ETicaretServer.Application;
using ETicaretServer.Application.Validators.Products;
using ETicaretServer.Infrastructure;
using ETicaretServer.Infrastructure.Enums;
using ETicaretServer.Infrastructure.Filters;
using ETicaretServer.Infrastructure.Services.Storage.Azure;
using ETicaretServer.Infrastructure.Services.Storage.Local;
using ETicaretServer.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
//builder.Services.AddStorage<AzureStorage>();
builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage<StorageType.Azure>();

builder.Services
    .AddCors(options => options
    .AddDefaultPolicy(policy => policy
    .WithOrigins("url","url")
    .AllowAnyHeader()
    .AllowAnyMethod()
    ));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, // olusturalan  token degerini kimlerin kullanacagini belirledigmiz yer
            ValidateIssuer = true, // olusturulacak token degerini kimin dagittigini ifade eder
            ValidateLifetime = true, // olusturulan token degerinin suresini kontrol eder
            ValidateIssuerSigningKey = true, // uretilen token in uygulamaya ait mi kontrol eder

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))

        };
    });

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).AddFluentValidation(
    configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true
    );
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authhorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"


    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id ="Bearer"
                }
            },
            new string[]{}
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

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
