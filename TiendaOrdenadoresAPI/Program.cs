using MVC_TiendaOrdenadores.Models;
using TiendaOrdenadoresAPI.Data;
using TiendaOrdenadoresAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(c =>
{
    var connectionString = builder.Configuration.GetConnectionString("TiendaOrdenadoresDBContext") ?? "";
    var dataDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\.."));
    connectionString = connectionString.Replace("|DataDirectory|", dataDirectory);

    return new AdoContext(connectionString);
});
builder.Services.AddScoped<IRepository<Componente>, RepositoryComponente>();
builder.Services.AddScoped<IRepository<Ordenador>, RepositoryOrdenador>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
