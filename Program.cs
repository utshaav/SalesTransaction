using Microsoft.EntityFrameworkCore;
using SalesTransaction.Data;
using SalesTransaction.Helpers;
using SalesTransaction.Interfaces;
using SalesTransaction.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllersWithViews();
services.AddDbContext<TestDBContext>(
    opt => opt.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("Default"))
);
services.AddScoped<ICustomerServices, CustomerServices>();
services.AddScoped<ISalesTransactionServices, SalesTransactionServices>();
services.AddScoped<IDropDownHelpers, DropDownHelpers>();
services.AddScoped<IProductServices, ProductServices>();

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
