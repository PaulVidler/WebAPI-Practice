using CityInfo.API;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApiPractice.DbContexts;
using WebApiPractice.Services;

// these packages were added for logging
// Serilog.AspNetCore
// Serilog.Sinks.File
// serilog.sinks.console
// the code below configures Serilog to send log messages to the console and to a file
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day) // this points to the file being created and the second argument makes it roll over every day to a new file
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args); // this is the new way to create a web application, with that, it sets up logging and configuration
// builder.Logging.ClearProviders(); // this will clear the default logging providers and will not show in the console, but the line below maybe overrides it?
// builder.Logging.AddConsole(); // send log/error messages to the console

builder.Host.UseSerilog();

// Add services to the container.

// builder.Services.AddControllers(); - Changing this rto configure responses in the api
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true; // this will return a 406 if the the header is asking for XML or another format that is not JSON
}).AddNewtonsoftJson()
  .AddXmlDataContractSerializerFormatters(); // adding this extension method will allow the api to return XML as well as JSON


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// this is the provider used in the file controller to manange file types
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

// transient will be created each time it is requested
// scoped will be created once per request
// singleton will be created the first time it is requested and will be used for every request after that

// this is the service that will be used to send emails, but it should implement an interface so it can be mocked
// for testing and changed if ever required. It is shown in the replaced line of code below. You will also need to add
// it in the DI in the controller constructor it is being called from
// builder.Services.AddTransient<LocalMailService>();
// builder.Services.AddTransient<IMailService, LocalMailService>(); - not used now and replaced with the code below

// you can also change the Mail service you want to use based on the environment, in the example below, it is Debug and then everything else
// this is done by the dropdown on the menu bar "Debug" next to "AnyCPU"
#if DEBUG
    builder.Services.AddTransient<IMailService, LocalMailService>();
#else
    builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

// add the data store to the DI container
builder.Services.AddSingleton<ICitiesDataStore, CitiesDataStore>();

builder.Services.AddDbContext<CityInfoContext>(dbContextOptions => dbContextOptions.UseSqlServer(
    builder.Configuration["ConnectionStrings:CityInfoDBConnectionString"]));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting(); // this must be added before UseAuthorization()

app.UseAuthorization();



app.UseEndpoints(endpoints => 
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
