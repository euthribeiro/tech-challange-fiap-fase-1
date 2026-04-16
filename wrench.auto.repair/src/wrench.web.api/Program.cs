using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories;
using wrench.auto.repair.ordem.servico.infra.Context;
using wrench.auto.repair.ordem.servico.infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Configurar o DbContext para usar PostgreSQL e pasta migration do contexto ser criada
// no projeto infra do contexto

// TODO: Mover string de conexão para secrets ou configuração externa
builder.Services.AddDbContext<OrdemServicoDbContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=db_wrench;Username=postgres;Password=postgres", 
        p => p.MigrationsAssembly("wrench.auto.repair.ordem.servico.infra"));
});

// TODO: COnfigurar demais contextos

// Registrar MediatR apontando para o Assembly da Camada Application
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(wrench.auto.repair.ordem.servico.application.UseCases.CriarOrdemServico.CriarOrdemServicoCommand).Assembly
));

// Registrar Dependências de Repositório (DI)
builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs");
}

app.UseHttpsRedirection();

// Registrar os endpoints de Ordem de Servico
wrench.web.api.Endpoints.OrdemServicoEndpoint.MapOrdemServicoEndpoints(app);

app.Run();