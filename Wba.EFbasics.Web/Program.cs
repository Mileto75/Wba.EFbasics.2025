using Microsoft.EntityFrameworkCore;
using Wba.EFbasics.Web.Data;
using Wba.EFbasics.Web.Services;
using Wba.EFbasics.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. => DI
//register the FormBuilderService
builder.Services.AddScoped<IFormBuilderService, FormBuilderService>();
//activate the server session
builder.Services.AddSession();
//add the database service
builder.Services.AddDbContext<HorseDbContext>(
    options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("HorseDbContext"))
    );
builder.Services.AddControllersWithViews();

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
//use sessions
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
