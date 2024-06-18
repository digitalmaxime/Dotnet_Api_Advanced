using StatelessWithUI;
using StatelessWithUI.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.ConfigurePipeline();

app.Run();

