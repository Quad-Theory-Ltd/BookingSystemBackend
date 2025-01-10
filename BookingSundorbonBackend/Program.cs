using BookingSundorbon.Features;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(name: "sundorbonBookingCors",
            policy => policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod()
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

//app.UseAuthorization();

app.MapControllers();

app.Run();
