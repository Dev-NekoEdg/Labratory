using Labratory.API.Utils;
using Labratory.Domain.Mapper;
using Labratory.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddProjectTransient();
builder.Services.AddProjectConfiguration(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "PublicCOR",
                      builder =>
                      {
                          // sin esta línea da problemas cunado se insertan datos.
                          builder.WithHeaders("*");
                          // Valida desde donde va a permitir ser cosumido el servicio.
                          builder.WithOrigins("*");
                          // Permite que se ejecuten todos los métodos Http.
                          builder.WithMethods("*");
                      });
});


builder.Services.AddDbContext<LaboratoryContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("laboratory"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{ }
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("PublicCOR");
app.Run();
