using Scalar.AspNetCore;
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

app.UseAuthorization();

app.MapControllers();

app.Run();