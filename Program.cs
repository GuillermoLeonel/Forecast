using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Forecast_Master.Models;
using Forecast_Master.Interfaces;
using Forecast_Master.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuración de Entity Framework Core
builder.Services.AddDbContext<ForecastContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero  // Reduce el tiempo de margen de error para mayor seguridad
    };
});

// Configuración de CORS (debe ser más restrictiva en producción)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecific",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("https://example.com")  // Especifica los orígenes permitidos
                         .AllowAnyMethod()
                         .AllowAnyHeader();
        });
});

// Registramos el servicio IParameterService con su implementación ParameterService
builder.Services.AddScoped<IReportService, ReportService>(); // Añadido para Reportes
builder.Services.AddScoped<IParameterService, ParameterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();  // Mantén HSTS en producción
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Configuración de CORS
app.UseCors("AllowSpecific");

// Configuración de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
