using BackendDevEvaluation.Contracts;
using BackendDevEvaluation.DbContextFile;
using BackendDevEvaluation.Repository;
using BackendDevEvaluation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ImageDBContext>();
builder.Services.AddSingleton<IKeyService, KeyService>();
builder.Services.AddTransient<IImageEntityRepository, ImageEntityRepository>();
builder.Services.AddTransient<IHelperService, HelperService>();


var helperService = builder.Services.BuildServiceProvider().GetService<IHelperService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
helperService.DeleteAllFiles();




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
