using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllers(); - Changing this rto configure responses in the api
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true; // this will return a 406 if the the header is asking for XML or another format that is not JSON
}).AddXmlDataContractSerializerFormatters(); // adding this extension method will allow the api to return XML as well as JSON


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// this is the provider used in the file controller to manange file types
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

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

app.UseEndpoints(endpoints => // this must be added after UseRouting()
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
