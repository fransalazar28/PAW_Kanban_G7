// K2.API/Program.cs  (API)
using Microsoft.EntityFrameworkCore;
using K.Data.MSSql;
using K.Repositories;
using K.Business;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<KanbanDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MVC + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (DEV)
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("ui", p => p
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// DI
builder.Services.AddScoped<IHistoriaRepository, HistoriaRepository>();
builder.Services.AddScoped<IHistoriaService, HistoriaService>();
builder.Services.AddScoped<IEtiquetaRepository, EtiquetaRepository>();
builder.Services.AddScoped<IEtiquetaService, EtiquetaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ui");
app.MapControllers();

app.Run();
