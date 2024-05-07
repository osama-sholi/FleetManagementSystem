using FleetManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new Exception("Connection string not found");
}


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<GeofenceService>( String => new GeofenceService(connectionString));
builder.Services.AddScoped<DriverService>( String => new DriverService(connectionString));
builder.Services.AddScoped<VehicleService>( String => new VehicleService(connectionString));
builder.Services.AddScoped<VehicleInfoService>(String => new VehicleInfoService(connectionString));
builder.Services.AddScoped<RouteHistoryService>(String => new RouteHistoryService(connectionString));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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

