using Microsoft.EntityFrameworkCore;
using SmartHouseSolutionsAPI;
using SmartHouseSolutionsAPI.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configure JSON serialization options to handle cycles
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

if(!builder.Environment.IsEnvironment("Testing"))
{
    // Add DbContext with connection string
    builder.Services.AddDbContext<SmartHouseSolutionsDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SmartHouseSolutionsDBString")));
}


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SmartHouse API", Version = "v1" });

    // Define the security scheme for API Key
    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "API Key needed to access the endpoints.",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "ApiKey", // The header name
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    // Make sure to require the API key on all endpoints
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement{
    {
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme{
            Reference = new Microsoft.OpenApi.Models.OpenApiReference{
                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                Id = "ApiKey"
            }
        }, new string[] { }
    }});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
