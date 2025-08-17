using System.Text.Json;
using System.Text.Json.Serialization;
using K.Data.MSSql;
using K.Business;
using K.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(o => o.AddPolicy("mvc", p =>
    p.WithOrigins(
        "https://localhost:7154",  
        "http://localhost:5132"    
    )
    .AllowAnyHeader()
    .AllowAnyMethod()

));


builder.Services.AddDbContext<KanbanDbContext>();


builder.Services.AddScoped<IHistoriaRepository, HistoriaRepository>();
builder.Services.AddScoped<IHistoriaService, HistoriaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("mvc");


app.UseAuthorization();

app.MapControllers();


app.MapGet("/ping", () => Results.Ok("pong"));

app.Run();
