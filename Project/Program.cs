using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelBookingDatabase")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

string sendGridApiKey = builder.Configuration["SendGrid:ApiKey"];
builder.Services.AddTransient<IEmailService>(provider => new EmailService(sendGridApiKey));

builder.Services.AddMemoryCache();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["Authentication:SecretForKey"]))
    };
});

builder.Services.AddAuthorization(options =>
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

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<ISuperAdminRepository, SuperAdminRepository>();

builder.Services.AddScoped<TokenServices>();
builder.Services.AddScoped<ValidationServices>();

var googleMapsSettings = builder.Configuration.GetSection("GoogleMaps");
builder.Services.AddScoped(sp => new GoogleMapsServices(googleMapsSettings["ApiKey"]));

builder.Services.AddHttpClient<IWeatherService, WeatherService>();

builder.Services.AddLogging();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelBookingSystem", Version = "v1" });

    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelBookingSystem v1");
        c.RoutePrefix = string.Empty;
    });
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    dbContext.InitializeDatabase();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
