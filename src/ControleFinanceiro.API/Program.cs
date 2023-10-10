using ControleFinanceiro.Domain.Adapters;
using ControleFinanceiro.Domain.Data;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiro.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IImportarService, ImportarService>();

builder.Services.AddScoped<DbSession>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IFaturaRepository, FaturaRepository>();
builder.Services.AddTransient<IImportacaoRepository, ImportacaoRepository>();
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
