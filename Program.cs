using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Repositorios;
using SistemaDeTarefas.Repositorios.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SistemaTarefasDBContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DataBase"),
        new MySqlServerVersion(new Version(8, 0, 32)) 
    ));

builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirOrigemAngular",
        policy => policy.WithOrigins("http://localhost:4200")
                         .AllowAnyMethod()
                         .AllowAnyHeader());
});


var app = builder.Build();

app.UseCors("PermitirOrigemAngular");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
