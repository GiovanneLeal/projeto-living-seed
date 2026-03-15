// Importações necessárias no topo do arquivo
using LivingSeed.API.Data; // Onde está o nosso AppDbContext
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ==========================================================
// 1. CONFIGURAÇÃO DO BANCO DE DADOS (Injeção de Dependência)
// ==========================================================

// Vamos no 'builder' (o construtor da aplicação), nas Configurações (Configuration),
// e pedimos para ele ler a ConnectionString com o apelido "DefaultConnection" lá do appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Agora ensinamos o sistema que o nosso AppDbContext existe.
// Dizemos a ele para usar o provedor do SQL Server e passamos o endereço (connectionString).
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// ==========================================================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
// Dizendo para a API aceitar pedidos de qualquer lugar (útil na fase de testes)
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Daqui para baixo é como a aplicação se comporta enquanto está rodando
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("PermitirTudo");
app.UseAuthorization();
app.MapControllers();

app.Run(); // Inicia a LivingSeed!
