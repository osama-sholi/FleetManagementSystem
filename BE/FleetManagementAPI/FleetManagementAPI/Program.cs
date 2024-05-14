using FleetManagementAPI.Services;
using FPro;

var builder = WebApplication.CreateBuilder(args);

const string frontEndOrigin = "http://localhost:4200"; ;

// Get the connection string from the appsettings.json file
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

try
{
    if (connectionString == null)
    {
        throw new Exception("Connection string not found");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    return;
}

// Services

// Configure JSON serialization options for controllers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Disable automatic property name conversion to camel case
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Configure CORS to allow requests from a specific origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins(frontEndOrigin)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Add additional services
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
    // Enable Swagger for documenting the API
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowSpecificOrigin");

// Enable WebSockets
app.UseWebSockets();

// Handle WebSocket connections
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

