using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using HotelBookingSystem.interfaces;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using System;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorPages();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("HotelBookingDatabase")));

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });

        services.AddControllers();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        string sendGridApiKey = configuration["SendGrid:ApiKey"];
        services.AddTransient<IEmailService>(provider => new EmailService(sendGridApiKey));

        services.AddMemoryCache();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Authentication:Issuer"],
                ValidAudience = configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(configuration["Authentication:SecretForKey"]))
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("CustomerZ", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("Role", "Customer");
            });
            options.AddPolicy("AdminZ", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("Role", "Admin");
            });
            options.AddPolicy("SuperAdminZ", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("Role", "SuperAdmin");
            });
        });

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<ISuperAdminRepository, SuperAdminRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<TokenServices>();
        services.AddScoped<ValidationServices>();

        var googleMapsSettings = configuration.GetSection("GoogleMaps");
        services.AddScoped(sp => new GoogleMapsServices(googleMapsSettings["ApiKey"]));

        services.AddHttpClient<IWeatherService, WeatherService>();

        services.AddLogging();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelBookingSystem", Version = "v1" });

            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            config.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new string[] {}
                }
            });
        });
    }
}
