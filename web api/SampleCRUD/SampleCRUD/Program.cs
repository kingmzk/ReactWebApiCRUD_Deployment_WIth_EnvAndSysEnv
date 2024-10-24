/*
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SampleCRUD.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


//builder.Services.AddDbContext<ProductDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});


//builder.Services.AddDbContext<ProductDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
//    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
//        maxRetryCount: 5, // Number of retry attempts
//        maxRetryDelay: TimeSpan.FromSeconds(10), // Maximum delay between retries
//        errorNumbersToAdd: null // You can specify specific error numbers to retry on or leave null for all transient errors
//    )));




// Now you can access the variables from the .env file like this
//Env.Load();

//Env.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));


//try
//{
//    var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");en

//    Console.WriteLine(connectionString);

//    builder.Services.AddDbContext<ProductDbContext>(options =>
//        options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
//            maxRetryCount: 5, // Number of retry attempts
//            maxRetryDelay: TimeSpan.FromSeconds(10), // Maximum delay between retries
//            errorNumbersToAdd: null // Retry on transient errors
//        )));

//    // Use your DbContext here
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Error: {ex.Message}");
//}



try
{
    var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

    // Add services to the container.
    builder.Services.AddControllers();

    // Configure DbContext with the connection string from environment variables
    builder.Services.AddDbContext<ProductDbContext>(options =>
        options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Number of retry attempts
            maxRetryDelay: TimeSpan.FromSeconds(10), // Maximum delay between retries
            errorNumbersToAdd: null // Retry on transient errors
        )));

    // Use your DbContext here
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
*/



using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SampleCRUD.Models;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();  // Uncomment to load environment variables

// Get the database connection string from environment variables
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Error: DB_CONNECTION_STRING is not set.");
    throw new InvalidOperationException("Database connection string is not set.");
}

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext with the connection string
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
        maxRetryCount: 5, // Number of retry attempts
        maxRetryDelay: TimeSpan.FromSeconds(10), // Maximum delay between retries
        errorNumbersToAdd: null // Retry on all transient errors
    )));

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS for all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");

// Enable Swagger in all environments
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
