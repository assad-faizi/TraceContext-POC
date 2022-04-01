using Shared;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTracing();
builder.Services.AddJaeger(config);

builder.Services.AddHttpClient("ServiceAHttpClient", x =>
{
    x.BaseAddress = new Uri(config.GetValue<string>("ServiceA:Uri"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
