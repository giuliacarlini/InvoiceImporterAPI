using ControleFinanceiroAPI.Data;
using ControleFinanceiroAPI.Interface;
using ControleFinanceiroAPI.Repositories;
using ControleFinanceiroAPI.Repositories.Interface;
using ControleFinanceiroAPI.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IImportar, ImportarService>();

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
