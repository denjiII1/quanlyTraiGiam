using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PrisonApi.Data;
using PrisonApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Context
builder.Services.AddDbContext<PrisonContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Authentication - JWT
// Validate that JWT settings exist to avoid null passed into Encoding.GetBytes
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Missing configuration 'Jwt:Key'");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Missing configuration 'Jwt:Issuer'");
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Missing configuration 'Jwt:Audience'");

var key = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// RUN MIGRATION + SEED
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PrisonContext>();
    db.Database.Migrate();
    SeedData.SeedAdmin(db);
}

// Configure the HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// NOTE: removed UseHttpsRedirection() to avoid automatic redirect to HTTPS
// which causes the browser to try HTTPS when the app is only listening on HTTP.
// If you want HTTPS in development, see instructions below.
 // app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
