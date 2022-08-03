using AA.CommoditiesDashboard.Data;
using AA.CommoditiesDashboard.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AnalyticsDashboardDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("AnalyticsDashboard")));

builder.Services.AddScoped<ICommoditiesService, CommoditiesService>();

var app = builder.Build();

using var scope = app.Services.CreateAsyncScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AnalyticsDashboardDbContext>();
dbContext.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(option =>
    option
        .WithOrigins("http://localhost:4200")
        .WithMethods("GET", "POST", "PUT")
        .AllowAnyHeader()
    );

app.UseAuthorization();

app.MapControllers();

app.Run();