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

// ── Core Configurations ───────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Database ── SQL Server Connection
builder.Services.AddDbContext<BuildManagerDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BuildManagerConnection"),
        sql => sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null)));

// ── AutoMapper Configuration ──
builder.Services.AddAutoMapper(typeof(BuildManagerMappingProfile).Assembly);

// ── JWT Authentication Configuration ──────────────────────────────────────
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

// ── CORS Configuration ────────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ── Swagger UI Engine ─────────────────────────────────────────────────────
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
        Description = "Enter token format: Bearer {your_jwt_token}"
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

// ── Dependency Injection Container (DI) ──────────────────────────────────
// Generic Repository
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

// Common Services
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

// Application Layer Services
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

// ── Request Pipeline Execution Order ─────────────────────────────────────

// 1. Exception Handler MUST always be first
app.UseMiddleware<ExceptionHandlingMiddleware>();

// 2. Swagger Engine Setup (Configured to output standard schema definitions)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        // Explicitly configurations force fallback version mappings
        options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
        {
            // Drops the strict patch identifier to keep Swagger UI renderer happy
            swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
        });
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Build Manager API v1");
        c.RoutePrefix = string.Empty; // Directly launches Swagger dashboard at the application root
    });
}

app.UseHttpsRedirection();

// 3. CORS rules must be active BEFORE Authentication checking
app.UseCors();

// 4. Identity validation block
app.UseAuthentication();
app.UseAuthorization();

// 5. Controller mapping maps requests securely to your route targets
app.MapControllers();

app.Run();