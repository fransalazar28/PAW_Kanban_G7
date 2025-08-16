using K.Data.MSSql;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(o => o.AddPolicy("mvc", p =>
    p.WithOrigins("https://localhost:7154", "http://localhost:5132")
     .AllowAnyHeader()
     .AllowAnyMethod()
));


builder.Services.AddDbContext<KanbanDbContext>();



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
