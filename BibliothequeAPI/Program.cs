using Microsoft.EntityFrameworkCore;
using BibliothequeAPI.Data;
using BibliothequeAPI.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<BibliothequeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<LivreRepository>();
builder.Services.AddScoped<UtilisateurRepository>();
builder.Services.AddScoped<EmpruntRepository>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // URL Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
