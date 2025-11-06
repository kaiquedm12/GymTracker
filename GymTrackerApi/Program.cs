using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using GymTrackerApi.Data;
using GymTrackerApi.Services.Interfaces;
using GymTrackerApi.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Carregar variáveis de ambiente (.env)
Env.Load();

// 2. Configurar banco PostgreSQL
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// 3. CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// 4. Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 5. Serviços e AutoMapper
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IExercicioService, ExercicioService>();
builder.Services.AddScoped<ITreinoService, TreinoService>();

var app = builder.Build();

// 6. Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 7. CORS
app.UseCors("AllowReactApp");

// ⚠️ Descomente só se for usar HTTPS no Postman
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
