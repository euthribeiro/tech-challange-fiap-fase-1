using Asp.Versioning;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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
using wrench.web.api.Middlewares;
using wrench.web.api.Options;
using wrench.web.api.Transformers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddDbContexts();

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

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(
        new RouteTokenTransformerConvention(
            new SlugifyParameterTransformer()));
});

builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.DefaultApiVersion = new ApiVersion(1.0);
    setupAction.ReportApiVersions = true;
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.MapOpenApi();
    app.MapScalarApiReference("docs-ui", options =>
    {
        options.Title = "Wrench API";
    });
}
else
{
    app.UseGlobalExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();