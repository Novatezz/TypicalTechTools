using TypicalTechTools.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using TypicalTechTools.Models.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Retrieve the database connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("Default");

// Register the database context with dependancy injection
// and configure the context to use SQL Server with the connection string
builder.Services.AddDbContext<TechToolsDBContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Register repositories with dependency injection
builder.Services.AddScoped<ICommentRepository,CommentRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configure session state
builder.Services.AddSession(c =>
{
    // Session timout 120 seconds
    c.IdleTimeout = TimeSpan.FromSeconds(120);
    // session cookies set to HTTP only
    c.Cookie.HttpOnly = true;
    // set session cookies to essential
    c.Cookie.IsEssential = true;
    //Set samesite policy
    c.Cookie.SameSite = SameSiteMode.Strict;
});

builder.Services.AddDistributedMemoryCache();

//Old CSV Parser builder - not used after database is configured
//builder.Services.AddSingleton<CsvParser>();

//Builds the web application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// redirects HTTP to HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable routing middleware
app.UseRouting();

//Enable session middleware
app.UseSession();

//Enable Authorisation middleware
app.UseAuthorization();

//Configure endpoint routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
