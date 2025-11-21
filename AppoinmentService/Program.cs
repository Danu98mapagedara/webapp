using AppoinmentService.Data;
using AppoinmentService.Services;
using Microsoft.EntityFrameworkCore;
using AppoinmentService.Kafka;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// Register KafkaProducer as singleton
builder.Services.AddSingleton(new KafkaProducer("localhost:9092"));

//add mysql db
builder.Services.AddDbContext<AppoinmentDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21)))
);
// Register Service Layer
builder.Services.AddScoped<AppoinmetServiceClass>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
