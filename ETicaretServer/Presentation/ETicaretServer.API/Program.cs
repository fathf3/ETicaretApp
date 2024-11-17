using ETicaretServer.API.Configurations.ColumnWriter;
using ETicaretServer.API.Extensions;
using ETicaretServer.API.Filter;
using ETicaretServer.Application;
using ETicaretServer.Application.Validators.Products;
using ETicaretServer.Infrastructure;
using ETicaretServer.Infrastructure.Enums;
using ETicaretServer.Infrastructure.Filters;
using ETicaretServer.Infrastructure.Services.Storage.Azure;
using ETicaretServer.Infrastructure.Services.Storage.Local;
using ETicaretServer.Persistence;
using ETicaretServer.SignalR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Services registration
builder.Services.AddHttpContextAccessor(); // Client dan gelen request  sonrası olusturaln Httpcontext nesnesine katmanlardan erismemizi saglar
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();

builder.Services.AddStorage<AzureStorage>();
//builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage<StorageType.Azure>();

builder.Services
    .AddCors(options => options
    .AddDefaultPolicy(policy => policy
    .WithOrigins("url", "url")
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters)
            => expires != null ? expires > DateTime.UtcNow : false,

            // JWT uzerinde  name claim e  karsilik gelen degeri User.Identity.Name propertiysinden elde edeilirz.
            NameClaimType = ClaimTypes.Name,
        };
    });

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs", needAutoCreateTable: true, columnOptions: new Dictionary<string, ColumnWriterBase>
    {
        { "message" , new RenderedMessageColumnWriter() },
        { "message_template", new MessageTemplateColumnWriter() },
        { "level", new LevelColumnWriter() },
        { "time_stamp" , new TimestampColumnWriter() },
        { "exception", new ExceptionColumnWriter() },
        { "log_event", new LogEventSerializedColumnWriter() },
        { "user_name" , new UsernameColumnWriter() }
    })
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
    .Enrich.FromLogContext()
    .MinimumLevel.Information() 
    .CreateLogger();
builder.Host.UseSerilog(log);

builder.Services.AddControllers(options => {
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<RolePermissionFilter>();
    })
    .AddFluentValidation(
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

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseStaticFiles();
app.UseSerilogRequestLogging(); // onceki middleware lari veya islemleri loglamaz!!!
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
});

app.MapControllers();
app.MapHubs(); // Hubları ekliyoruz!
app.Run();
