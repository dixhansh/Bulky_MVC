using BulkyWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registering EntityFramework Service and connecting it with SQLServer
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));


/*Note that EntityFramework is not able to migrate changes in both servers simultaneously*/
//The following code is just to demonstrate how to setup connection with mySQL server

/*//Reteriving MySql Connection String from appsettings.json
var MysqlConnStr = builder.Configuration.GetConnectionString("MySqlConnection");
//Registering EntityFramerok Service and connecting it with MySQLServer
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(MysqlConnStr,
    ServerVersion.AutoDetect(MysqlConnStr)));*/



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
