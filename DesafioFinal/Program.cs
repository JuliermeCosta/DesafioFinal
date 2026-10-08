using DesafioFinal.Config;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //Ignores propriedades com valor null no retorno do JSON
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

//Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Registra o Handler vindo da camada Application
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//Injeções de dependência
//1. Registra o DbContext (EF Core)
//Configura o SQLite In-Memory
builder.Services.AddInMemoryDatabaseConfiguration();

//2. Repositórios (Adapter Out)
//3. Serviços de Domínio / Validadores
//4. Casos de Uso / Facades (Application / Ports In)
builder.Services.AddDependencyInjection();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/Config/{documentName}.json");

    //Ativa a interface do Swagger UI
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/Config/v1.json", "Sua API v1");
        options.RoutePrefix = "swagger"; //Acessível em /swagger
    });
}
else
{
    // Em produção, exige HTTPS
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.UseAutoMigrations();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
