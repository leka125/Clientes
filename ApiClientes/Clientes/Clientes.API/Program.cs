using Clientes.API.Examples;
using Clientes.DTO;
using Clientes.Repositories;
using Clientes.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Se adiciona el servicio de controladores
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Clientes API",
        Version = "v1",
        Description = "API para consulta de clientes",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Equipo de Desarrollo",
            Email = "dev@empresa.com"
        }
    });

    // Incluir comentarios XML para documentación
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);

    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Configurar esquemas de ejemplo
    c.MapType<ClienteDTO>(() => new OpenApiSchema
    {
        Example = OpenApiAnyFactory.CreateFromJson(
            System.Text.Json.JsonSerializer.Serialize(ClienteResponseExamples.GetClienteExample()))
    });
});


// Configurar CORS para Angular
var corsAllowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string>();
var originsArray = corsAllowedOrigins?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                   ?? new[] { "http://localhost:4200" }; // Valor por defecto

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClient", policy =>
    {
        policy.WithOrigins(originsArray)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Configurar servicios de repositorio y negocio
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AngularClient");

app.UseAuthorization();

app.MapControllers();

app.Run();