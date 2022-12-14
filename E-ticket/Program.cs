using E_ticket.Data;
using E_ticket.Data.Cart;
using E_ticket.Data.Repository;
using E_ticket.Data.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IActorsService, ActorsService>();
builder.Services.AddScoped<IProducersService,ProducersService>();
builder.Services.AddScoped<ICinemasService,CinemasService>();
builder.Services.AddScoped<IMoviesService,MoviesService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
//configure session
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

builder.Services.AddSession();


builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
app.UseSession();
// initialize seed data
AppDbInitializer.Seed(app);

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
