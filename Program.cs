using AI_Friendly_Calendar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AI_Friendly_Calendar.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register your DbContext (adjust connection string name if needed)
builder.Services.AddDbContext<CalendarDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();

// Add Swagger generator
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AI Friendly Calendar API",
        Version = "v1",
        Description = "API to test login and register endpoints"
    });
    // 👇 Tell Swagger to include your custom group called "tools"
    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (docName == "v1") return true; // include all
        return apiDesc.GroupName == docName;
    });
});

builder.Services.AddScoped<IFreeSlotService, FreeSlotService>();


var app = builder.Build();

// Enable Swagger middleware only in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AI Friendly Calendar API V1");
        c.RoutePrefix = ""; // Swagger UI available at app root
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (!app.Environment.IsDevelopment())
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}

// Enable SPA fallback to index.html
app.MapFallbackToFile("index.html");
app.UseStaticFiles();
app.UseDefaultFiles();


app.Run();
