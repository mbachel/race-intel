using RaceIntel.Api.Nascar.Services;
using RaceIntel.Api.Admin;
using RaceIntel.Api.Data;
using Microsoft.EntityFrameworkCore;

//=== BUILD PHASE BEGIN ===

var builder = WebApplication.CreateBuilder(args);

//scan for [ApiController] and add those controllers to the service collection
builder.Services.AddControllers();

//add services for nascar
builder.Services.AddHttpClient<NascarApiClient>();
builder.Services.AddHttpClient<NascarHistoricalApiClient>();
builder.Services.AddScoped<AdminKeyAuthFilter>();
builder.Services.AddSingleton<NascarCacheService>();
builder.Services.AddSingleton<NascarLiveRaceDetector>();
builder.Services.AddHostedService<NascarPollingService>();
builder.Services.AddDbContext<RaceIntelDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//allow frontend to call api from different origin
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// === BUILD PHASE END ===

// === RUN PHASE BEGIN ===

//build the app and configure the HTTP request pipeline
var app = builder.Build();

//enable CORS policy
app.UseCors();

//map controller routes
app.MapControllers();

//start listening for API requests
app.Run();

// === RUN PHASE END ===