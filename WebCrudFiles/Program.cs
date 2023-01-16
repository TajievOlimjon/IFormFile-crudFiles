using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebCrudFiles.Data;
using WebCrudFiles.Mappers;
using WebCrudFiles.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseSqlite(connection));

builder.Services.AddScoped<IFileService,FileService>();
builder.Services.AddAutoMapper(typeof(EntitiesMap));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
