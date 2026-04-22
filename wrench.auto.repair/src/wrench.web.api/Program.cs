using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using wrench.auto.repair.autenticacao.infra;
using wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories;
using wrench.auto.repair.ordem.servico.infra.Context;
using wrench.auto.repair.ordem.servico.infra.Repositories;
using wrench.web.api.Options;

var builder = WebApplication.CreateBuilder(args);

var isDevelopment = builder.Environment.IsDevelopment();

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services.AddOpenApi();

// Configurar o DbContext para usar PostgreSQL e pasta migration do contexto ser criada
// no projeto infra do contexto

// TODO: Mover string de conexão para secrets ou configuração externa
builder.Services.AddDbContext<OrdemServicoDbContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=db_wrench;Username=postgres;Password=postgres",
        p => p.MigrationsAssembly("wrench.auto.repair.ordem.servico.infra"));
});

builder.Services.AddDbContext<AutenticacaoContext>((serviceProvider, dbContextOptionsBuilder) =>
{
    var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

    var connectionString = builder.Configuration.GetConnectionString("Database");

    //dbContextOptionsBuilder.UseNpgsql(connectionString, sqlAction =>
    //{
    //    sqlAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
    //    sqlAction.CommandTimeout(databaseOptions.CommandTimeout);
    //});

    dbContextOptionsBuilder.UseSqlServer(connectionString, sqlAction =>
    {
        sqlAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
        sqlAction.CommandTimeout(databaseOptions.CommandTimeout);
    });

    dbContextOptionsBuilder.EnableDetailedErrors(isDevelopment);
    dbContextOptionsBuilder.EnableSensitiveDataLogging(isDevelopment);
});

// TODO: COnfigurar demais contextos

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    config.RegisterServicesFromAssemblies(typeof(wrench.auto.repair.ordem.servico.application.UseCases.CriarOrdemServico.CriarOrdemServicoCommand).Assembly);
});

// Registrar Dependências de Repositório (DI)
builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs");
}

app.UseHttpsRedirection();

// Registrar os endpoints de Ordem de Servico
app.MapControllers();

app.Run();