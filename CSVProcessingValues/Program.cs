using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CSVProcessingValues.Contexts;
using CSVProcessingValues.Repositories;
using CSVProcessingValues.Repositories.ResultRepository;
using CSVProcessingValues.Repositories.ValueRepository;
using CSVProcessingValues.Services;
using CSVProcessingValues.Services.ResultService;
using CSVProcessingValues.Services.ValueService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Formatting = Formatting.Indented;
    });

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connection));
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IValueService, ValueService>();
builder.Services.AddScoped<IValueRepository, ValueRepository>();
builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();