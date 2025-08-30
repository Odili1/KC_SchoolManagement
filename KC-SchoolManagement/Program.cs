using System.Text;
using System.Text.Json.Serialization;
using KC_SchoolManagement.Domain;
using KC_SchoolManagement.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddDbContext<KC_SchoolManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnect")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Define the security scheme for JWT Bearer tokens in Swaggger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization", // The name of the header
        Type = SecuritySchemeType.Http, // The type of security scheme
        Scheme = "Bearer", // The scheme name
        In = ParameterLocation.Header, // Where the token is placed (header)
        Description = "JWT Authorization header that uses a bearer scheme. Enter token in the text below"
    });

    // Add a global security requirement that applies to all API endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" // Reference the security scheme defined above
                }
            },
            Array.Empty<string>() // No specific scopes required
        }
    });
});

// Configure Authentication services to use JWT Bearer tokens
builder.Services.AddAuthentication(options =>
{
    // Set the default authentication scheme to JWT Bearer
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => // Add JWT Bearer token handling
{
    // COnfigure how to validate incoming JWT tokens
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Validate the token issuer
        ValidateAudience = true, // Validate the token audience
        ValidateLifetime = true, // Check if the token is not expired
        ValidateIssuerSigningKey = true, // Validate the signing key
        ValidIssuer = builder.Configuration["Issuer"], // Get valid issuer from configuration
        ValidAudience = builder.Configuration["Audience"], // Get valid audience from configuratio
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Key"])), // Get signing key from configuration
        ClockSkew = TimeSpan.Zero // No tolerance for expiration time differences
    };
});


// COnfigure Authorization policies based on user roles
builder.Services.AddAuthorizationBuilder()
    // Create an "Admin" policy that requires the user to have the Admin role
    .AddPolicy("Admin", policy => policy.RequireRole(UserType.Admin.ToString()))
    // Create a "Student" policy that requires the user to have the User role
    .AddPolicy("User", policy => policy.RequireRole(UserType.Student.ToString()))
    // Create a "Teacher" policy that requires the user to have the User role
    .AddPolicy("User", policy => policy.RequireRole(UserType.Teacher.ToString()));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
