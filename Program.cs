using FleetProLayered_Architecture.DataAccess.Repositories;
using IAB_251_Assessment2.Application.Interfaces;
using IAB_251_Assessment2.Application.Services;
using IAB_251_Assessment2.BusinessLogic.Interfaces;
using IAB_251_Assessment2.BusinessLogic.Services;
using IAB_251_Assessment2.DataAccess.Data;
using IAB_251_Assessment2.DataAccess.Interfaces;
using IAB_251_Assessment2.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserManagementDBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.


// User Registration Services
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddScoped<IUserRegistrationAppService, UserRegistrationAppService>();

// Quotation Services
builder.Services.AddScoped<IQuoteRepository, EFQuoteRepository>();
builder.Services.AddScoped<IQuotationService, QuotationService>();
builder.Services.AddScoped<IQuoteAppService, QuoteAppService>();


builder.Services.AddRazorPages();

builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Users/UserLogin";
        options.AccessDeniedPath = "/AccessDenied";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<UserManagementDBContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
