using ControleFinanceiro.Application;
using ControleFinanceiro.Domain.Adapters;
using ControleFinanceiro.Domain.Adapters.Repository;
using ControleFinanceiro.Domain.Data;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiro.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IConverterService, ConverterService>();

builder.Services.AddScoped<DbSession>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IFaturaRepository, FaturaRepository>();
builder.Services.AddTransient<ILancamentoImportacaoRepository, LancamentoImportacaoRepository>();
builder.Services.AddTransient<ILancamentoRepository, LancamentoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
