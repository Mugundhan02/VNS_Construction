using System.Text;
using AutoMapper;
using BuildManager.Contexts;
using BuildManager.Interfaces;
using BuildManager.Mappings;
using BuildManager.Middlewares;
using BuildManager.Repositories;
using BuildManager.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Database ── SQL Server Connection
builder.Services.AddDbContext<BuildManagerDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BuildManagerConnection"),
        sql => sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null)));

// ── AutoMapper (Updated with proper assembly-scanning syntax) ──
builder.Services.AddAutoMapper(typeof(BuildManagerMappingProfile).Assembly);

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "BuildManager@VNSConstruction#SecretKey2024!";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "BuildManager";

builder.Services
    .AddAuthentication(options =>
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
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

// Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OwnerOnly", p => p.RequireRole("Owner"));
    options.AddPolicy("AdminOrOwner", p => p.RequireRole("Owner", "Admin"));
    options.AddPolicy("AllRoles", p => p.RequireRole("Owner", "Admin", "User"));
});

// CORS ── Configured as a Default Policy matching Timesheet style
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Swagger with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Build Manager API", Version = "v1" });
    var bearerScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Enter: Bearer {token}"
    };
    options.AddSecurityDefinition("Bearer", bearerScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() }
    });
});

// Generic Repository
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

// Common Services
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

// Application Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyUserService, CompanyUserService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ISubContractorService, SubContractorService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IJobWorkService, JobWorkService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

var app = builder.Build();

// Global Exception Handler Middleware — MUST be first
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Build Manager API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// CORS MUST run right before Authentication pipeline
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();