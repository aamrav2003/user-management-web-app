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

app.Run();

//app.UseHttpsRedirection();

//app.UseRouting();

//app.UseAuthorization();

//app.MapStaticAssets();
//app.MapRazorPages()
//   .WithStaticAssets();

//app.Run();
