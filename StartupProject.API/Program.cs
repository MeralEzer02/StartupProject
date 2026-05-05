using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StartupProject.Data;
using StartupProject.Data.Repositories;
using StartupProject.API.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS Politikasý
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Generic Repository kaydý
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Unit of Work kaydý
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AutoMapper Kaydý
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<MapProfile>();
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
