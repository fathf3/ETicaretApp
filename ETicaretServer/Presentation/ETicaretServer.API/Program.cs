using ETicaretServer.Application;
using ETicaretServer.Application.Validators.Products;
using ETicaretServer.Infrastructure;
using ETicaretServer.Infrastructure.Enums;
using ETicaretServer.Infrastructure.Filters;
using ETicaretServer.Infrastructure.Services.Storage.Azure;
using ETicaretServer.Infrastructure.Services.Storage.Local;
using ETicaretServer.Persistence;
using FluentValidation.AspNetCore;

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




builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).AddFluentValidation(
    configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true
    );
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
