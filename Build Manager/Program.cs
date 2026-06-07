using System.Text;
using AutoMapper;
using BuildManager.Data;
using BuildManager.Mappings;
using BuildManager.Middleware;
using BuildManager.Services.Implementations;
using BuildManager.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ─────────────────────────────────────────────────────────────────────────────
// Database
// ─────────────────────────────────────────────────────────────────────────────
builder.Services.AddDbContext<BuildManagerDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BuildManagerConnection"),
        sql => sql.EnableRetryOnFailure(
            maxRetryCount:     5,
            maxRetryDelay:     TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null)));

// ─────────────────────────────────────────────────────────────────────────────
// AutoMapper  — manual registration (no DI extension package needed)
// ─────────────────────────────────────────────────────────────────────────────
builder.Services.AddSingleton(provider =>
    new MapperConfiguration(cfg =>
        cfg.AddProfile<BuildManagerMappingProfile>())
    .CreateMapper());

// ─────────────────────────────────────────────────────────────────────────────
// JWT Authentication
// ─────────────────────────────────────────────────────────────────────────────
var jwtKey    = builder.Configuration["Jwt:Key"]    ?? "BuildManager@VNSConstruction#SecretKey2024!";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "BuildManager";

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey  = true,
            ValidIssuer              = jwtIssuer,
            ValidAudience            = jwtIssuer,
            IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew                = TimeSpan.Zero
        };
    });

// ─────────────────────────────────────────────────────────────────────────────
// Authorization Policies  (Owner > Admin > User)
// ─────────────────────────────────────────────────────────────────────────────
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OwnerOnly",    p => p.RequireRole("Owner"));
    options.AddPolicy("AdminOrOwner", p => p.RequireRole("Owner", "Admin"));
    options.AddPolicy("AllRoles",     p => p.RequireRole("Owner", "Admin", "User"));
});

// ─────────────────────────────────────────────────────────────────────────────
// CORS
// ─────────────────────────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("BuildManagerCorsPolicy", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// ─────────────────────────────────────────────────────────────────────────────
// Swagger / OpenAPI
// ─────────────────────────────────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title       = "Build Manager API",
        Version     = "v1",
        Description = "Construction management REST API for VNS Construction"
    });

    var bearerScheme = new OpenApiSecurityScheme
    {
        Name         = "Authorization",
        Description  = "Enter: Bearer {token}",
        In           = ParameterLocation.Header,
        Type         = SecuritySchemeType.ApiKey,
        Scheme       = "Bearer",
        BearerFormat = "JWT"
    };
    options.AddSecurityDefinition("Bearer", bearerScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// ─────────────────────────────────────────────────────────────────────────────
// Application Services  — scoped per HTTP request
// ─────────────────────────────────────────────────────────────────────────────

// Auth
builder.Services.AddScoped<IAuthService,           AuthService>();

// Masters
builder.Services.AddScoped<ICompanyService,        CompanyService>();
builder.Services.AddScoped<ICompanyUserService,    CompanyUserService>();
builder.Services.AddScoped<IClientService,         ClientService>();
builder.Services.AddScoped<ISupplierService,       SupplierService>();
builder.Services.AddScoped<ISubContractorService,  SubContractorService>();
builder.Services.AddScoped<IMaterialService,       MaterialService>();
builder.Services.AddScoped<IJobWorkService,        JobWorkService>();
builder.Services.AddScoped<ILookupService,         LookupService>();

// Transactions
builder.Services.AddScoped<ITransactionService,    TransactionService>();

builder.Services.AddControllers();

// ─────────────────────────────────────────────────────────────────────────────
// Pipeline
// ─────────────────────────────────────────────────────────────────────────────
var app = builder.Build();

// Global exception handler — must be the first middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Build Manager API v1");
        c.RoutePrefix = string.Empty;   // Swagger UI at https://localhost:{port}/
    });
}

app.UseHttpsRedirection();
app.UseCors("BuildManagerCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
