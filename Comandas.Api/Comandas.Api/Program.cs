using Comandas.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Configurar o contexto do banco de dados para usar o InMemoryDatabase
builder.Services.AddDbContext<ComandaDbContext>(options =>
options.UseSqlite("Data Source=comandas.db")
);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona o serviço CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MinhaPolitica", policy =>
    {
        policy
        .AllowAnyHeader() // Permite qualquer cabeçalho
        .AllowAnyMethod() // Permite qualquer método HTTP
        .AllowCredentials()// Permite o uso de credenciais (cookies, cabeçalhos de autorização, etc.)
        .SetIsOriginAllowed(origin => true); // Origens permitidas
        
    });
});

var app = builder.Build();

// Configura o middleware CORS

app.Urls.Add("https://localhost:7004");

app.UseCors("MinhaPolitica");

// Configure the HTTP request pipeline.
///if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
