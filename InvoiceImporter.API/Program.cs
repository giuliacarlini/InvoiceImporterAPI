using InvoiceImporter.API.Settings;
using InvoiceImporter.Domain.Adapters;
using InvoiceImporter.Domain.Adapters.Repository;
using InvoiceImporter.Domain.Handlers;
using InvoiceImporter.Domain.Infra.Context;
using InvoiceImporter.Domain.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

var appSettings = new AppSettings();

builder.Configuration.Bind(appSettings);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(appSettings.ConnectionStrings?.SQLServer));

AddServices(builder, configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("JwtSettings", options))
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("CookieSettings", options));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();

static void AddServices(WebApplicationBuilder builder, ConfigurationManager configuration)
{
    builder.Services.AddTransient<InvoiceHandler, InvoiceHandler>();
    builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
    builder.Services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
    builder.Services.Configure<AppSettings>(configuration);
    builder.Services.AddScoped<AppSettings, AppSettings>();
    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
}