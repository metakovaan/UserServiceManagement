using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserServiceManagement.Contracts.Configurations;
using UserServiceManagement.Contracts.Repositories;
using UserServiceManagement.Contracts.Services;
using UserServiceManagement.Data.Configurations;
using UserServiceManagement.Data.Contexts;
using UserServiceManagement.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("allow-user-service-web-app",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddDbContext<ApplicationDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
            optionsBuilder =>
            {
                optionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                optionsBuilder.EnableRetryOnFailure();
            })
        .EnableDetailedErrors();
});

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//Register Services
builder.Services.AddScoped<IUserService, UserServiceManagement.Services.Services.UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddSingleton<ICloudinaryService, CloudinaryService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UserServiceManagementApi",
        Version = "v1"
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.CustomSchemaIds(type => type.ToString());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("allow-user-service-web-app");

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "uerServiceManagement v1"));

app.UseRouting();
app.MapControllers();

app.Run();

