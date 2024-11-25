using ETicaretServer.Application;
using ETicaretServer.Infrastructure;
using ETicaretServer.Infrastructure.Services.Storage.Azure;
using ETicaretServer.Persistence;
using ETicaretServer.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor(); // Client dan gelen request  sonrası olusturaln Httpcontext nesnesine katmanlardan erismemizi saglar
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();
builder.Services.AddStorage<AzureStorage>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
