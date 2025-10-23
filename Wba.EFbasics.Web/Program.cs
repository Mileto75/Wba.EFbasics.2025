using Microsoft.EntityFrameworkCore;
using Wba.EFbasics.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
