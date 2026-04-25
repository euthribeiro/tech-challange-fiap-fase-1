using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using wrench.auto.repair.autenticacao.infra;
using wrench.auto.repair.estoque.domain.Interfaces.Repositories;
using wrench.auto.repair.estoque.infra.Context;
using wrench.auto.repair.estoque.infra.Repositories;
using wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories;
using wrench.auto.repair.ordem.servico.infra.Context;
using wrench.auto.repair.ordem.servico.infra.Repositories;
using wrench.auto.repair.autenticacao.application.Commands;
using wrench.auto.repair.autenticacao.domain.Data;
using wrench.auto.repair.autenticacao.infra.Repositories;
using wrench.auto.repair.core.Mediator;
using wrench.auto.repair.autenticacao.application.Extensions;
using wrench.auto.repair.autenticacao.infra.Extensions;
using wrench.auto.repair.cadastro.application.Extensions;
using wrench.auto.repair.cadastro.infra.Extensions;
using wrench.auto.repair.core.Extensions;
using wrench.auto.repair.estoque.application.Extensions;
using wrench.auto.repair.estoque.infra.Extensions;
using wrench.auto.repair.ordem.servico.application.Extensions;
using wrench.auto.repair.ordem.servico.infra.Extensions;
using wrench.web.api.Configuration;
using wrench.web.api.Contexts;
using wrench.web.api.Docs;
using wrench.web.api.Options;

var builder = WebApplication.CreateBuilder(args);

var isDevelopment = builder.Environment.IsDevelopment();

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureAuthentication(builder.Configuration);

builder.AddContexts();

// TODO: COnfigurar demais contextos
builder.Services.AddDbContext<PecaDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=db_wrench;Username=postgres;Password=postgres",
        p => p.MigrationsAssembly("wrench.auto.repair.estoque.infra")));
builder.AddContexts();

// Registrar Dependências de Repositório (DI)
builder.Services.AddScoped<IPecaRepository, PecaRepository>();

builder.Services
    .AddCore()
    .AddAutenticacaoApplication()
    .AddAutenticacaoInfra()
    .AddCadastroApplication()
    .AddCadastroInfra()
    .AddEstoqueApplication()
    .AddEstoqueInfra()
    .AddOrdemServicoApplication()
    .AddOrdemServicoInfra();

builder.Services.ConfigureOpenApi();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("docs-ui", options =>
    {
        options.Title = "Wrench API";
    });
}

app.UseHttpsRedirection();

// Registrar os endpoints de Ordem de Servico
app.UseAuthorization();

app.MapControllers();

app.Run();