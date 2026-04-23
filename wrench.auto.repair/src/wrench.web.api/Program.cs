using Scalar.AspNetCore;
using wrench.auto.repair.autenticacao.application.Commands;
using wrench.auto.repair.autenticacao.domain.Data;
using wrench.auto.repair.autenticacao.infra.Repositories;
using wrench.auto.repair.core.Mediator;
using wrench.web.api.Contexts;
using wrench.web.api.Options;

var builder = WebApplication.CreateBuilder(args);

var isDevelopment = builder.Environment.IsDevelopment();

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services.AddOpenApi();

builder.AddContexts();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(UsuarioCommandHandler).Assembly);
});

builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs");
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();