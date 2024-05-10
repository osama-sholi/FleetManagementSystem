using FleetManagementAPI.Services;
using FPro;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new Exception("Connection string not found");
}


// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<WebSocketService>();
builder.Services.AddScoped<GeofenceService>( String => new GeofenceService(connectionString));
builder.Services.AddScoped<DriverService>( String => new DriverService(connectionString));
builder.Services.AddScoped<VehicleService>( String => new VehicleService(connectionString));
builder.Services.AddScoped<VehicleInfoService>(String => new VehicleInfoService(connectionString));
builder.Services.AddScoped<RouteHistoryService>(String => new RouteHistoryService(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseWebSockets();
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/api/ws")
    {
        try
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocketService webSocketService = app.Services.GetRequiredService<WebSocketService>();
                await webSocketService.HandleConnection(context);
            }
            else
            {
                throw new Exception("Request is not a WebSocket request");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            GVAR gvar = new GVAR();
            gvar.DicOfDic["Tags"] = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
            gvar.DicOfDic["Tags"]["STS"] = "0";

            await context.Response.WriteAsJsonAsync(gvar);
        }
    }
    else
    {
        await next();
    }
});



app.UseAuthorization();

app.MapControllers();

app.Run();

