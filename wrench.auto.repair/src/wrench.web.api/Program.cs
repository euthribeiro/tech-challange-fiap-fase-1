using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using wrench.auto.repair.autenticacao.infra;
using wrench.web.api.Options;

var builder = WebApplication.CreateBuilder(args);

var isDevelopment = builder.Environment.IsDevelopment();

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services.AddOpenApi();

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();