using Microsoft.EntityFrameworkCore;
using CodingEvents.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["CodingEvents:ConnectionString"];
var Major = builder.Configuration.GetValue<int>("MySQL:Major");
var Minor = builder.Configuration.GetValue<int>("MySQL:Minor");
var Build = builder.Configuration.GetValue<int>("MySQL:Build");
var serverVersion = new MySqlServerVersion(
      new Version(Major, Minor, Build));

// Add services to the container.
builder.Services.AddControllersWithViews();
// register persistent data store as a service
builder.Services.AddDbContext<EventDbContext>(
    dbContextOptions => dbContextOptions.UseMySql(
        connectionString, serverVersion));

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
