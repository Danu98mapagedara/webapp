using Microsoft.EntityFrameworkCore;
using PatientService.Data;
using PatientService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//add mysql db
builder.Services.AddDbContext<PatientDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21)))
);
// Register Service Layer
builder.Services.AddScoped<PatientServiceClass>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

// Map Controllers
app.MapControllers();
app.Run();


