using BookingSundorbon.Features;
using BookingSundorbonBackend.Hubs;
using BookingSundorbonBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddSignalR();
builder.Services.AddScoped<INotificationHubService, NotificationHubService>();

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(name: "sundorbonBookingCors",
            policy => policy.WithOrigins("http://www.sundarbancargoservicesltd.uk",
            "https://sundarbancargoservicesltd.uk",
            "https://www.sundarbancargoservicesltd.uk",
            "https://www.sundarbancargoservicesltd.uk",
            "http://www.sundarbancargoservicesltd.uk/",
            "https://www.sundarbancargoservicesltd.uk/",
            "http://www.sundarbancargoservicesltd.uk/api",
            "http://www.sundarbancargoservicesltd.uk/api/",
            "https://www.sundarbancargoservicesltd.uk/api/", 
            "http://localhost:3000",
            "http://www.sundarbancargo.com/",
            "https://www.sundarbancargo.com/",
            "http://localhost:3000",
            "http://localhost:3001",
            "http://202.126.122.82:33",
            "http://202.126.122.82:33/",
            "http://202.126.122.82:33/api",
            "https://202.126.122.82:33",
            "https://202.126.122.82:33/",
            "https://202.126.122.82:33/api",
            "http://202.126.122.82:33/api/",
            "https://202.126.122.82:33/api/",
            "http://sundarbancargo.com", 
            "http://sundarbancargo.com/api",
            "https://sundarbancargo.com", 
            "https://sundarbancargo.com/api", 
            "https://sundarbancargo.com/", 
            "https://sundarbancargo.com/api", 
            "https://sundarbancargo.com:500", 
            "https://sundarbancargo.com:500/api",
            "https://www.sundarbancargo.com/",
            "https://sundarbancargo.com:500/api/")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            );
    }
    );

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Pls insert token",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var config = builder.Configuration["Jwt:Secret"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
 )
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config))
    };

    opt.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
    options.AddPolicy("AppUserAndAdmin", policy => policy.RequireRole("User", "Admin"));
    // Add other policies as needed


    //options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    //options.AddPolicy("RequireSuperAdminRole", policy => policy.RequireRole("Super Admin"));
    //options.AddPolicy("RequireAgentRole", policy => policy.RequireRole("Agent"));
    //options.AddPolicy("RequireClientRole", policy => policy.RequireRole("Client"));
    //options.AddPolicy("RequireEmployeeScannerRole", policy => policy.RequireRole("Employee - Scanner"));
    //options.AddPolicy("RequireEmployeeBookingRole", policy => policy.RequireRole("Employee - Booking"));
    //options.AddPolicy("RequireEmployeeAccountsRole", policy => policy.RequireRole("Employee - Accounts"));
    //options.AddPolicy("RequireBDPartnersRole", policy => policy.RequireRole("BD Partners"));

    //// Multiple
    //options.AddPolicy("AppUserAndAdmin", policy => policy.RequireRole("User", "Admin"));
    //options.AddPolicy("AdminAndSuperAdmin", policy => policy.RequireRole("Admin", "Super Admin"));


});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("sundorbonBookingCors");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/api/hubs/notification");

app.Run();
