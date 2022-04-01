using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenTelemetryTracing(builder =>
{
    builder.AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddSource("Client.WebApi")
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Client.WebApi"))
        .AddJaegerExporter(opts =>
        {
            opts.AgentHost = config["Jaeger:AgentHost"];
            opts.AgentPort = Convert.ToInt32(config["Jaeger:AgentPort"]);
            opts.ExportProcessorType = ExportProcessorType.Simple;
        });
});
builder.Services.AddHttpClient("ServiceAHttpClient", x =>
{
    x.BaseAddress = new Uri(config.GetValue<string>("ServiceA:Uri"));
});

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
