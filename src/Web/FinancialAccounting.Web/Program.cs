using FinancialAccounting.Core;
using FinancialAccounting.Dara.PostgreSql;
using FinancialAccounting.Web.Authentication;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = builder.Configuration;

services.AddEndpointsApiExplorer().AddSwaggerGen();

services
    .AddHttpContextAccessor()
    .AddCore()
    .AddUserContext()
    .AddPostgreSql(x => x.ConnectionString = configuration.GetConnectionString("DbConnectionString"))
    .AddCors(options => options.AddPolicy(
        "AllowOrigin",
        policyBuilder => policyBuilder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()));

services.AddControllers();
    
var app = builder.Build();
{
    using (var scope = app.Services.CreateScope())
    {
        var migrator = scope.ServiceProvider.GetRequiredService<DbMigrator>();

        await migrator.MigrateAsync();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowOrigin");

    app.MapControllers();

    app.Run();
}
    
    