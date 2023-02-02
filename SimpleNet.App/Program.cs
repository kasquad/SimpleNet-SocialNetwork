using SimpleNet.OptionsSetup;
using SimpleNet.Persistence;
using SimpleNet.Application;
using SimpleNet.DependencyInjection;
using SimpleNet.Middlewares;
using SimpleNet.Services;

var builder = WebApplication.CreateBuilder(args);
// Get configuration reference
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);

builder.Services.AddControllersWithViews();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddCookieBasedJwtAuth(configuration);

builder.Services.AddTransient<RedirectNonAuthMiddleware>();

builder.Services.AddAppServices();

var app = builder.Build();

// Database initialization
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {   
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        Console.WriteLine(exception.Message);
    }
}

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

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RedirectNonAuthMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Map(new PathString("/"), () =>
{
});

app.Run();