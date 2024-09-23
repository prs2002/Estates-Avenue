using DotNetBackend.Models;
using DotNetBackend.Repositories;
using DotNetBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DotNetBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")  // Adjust to your frontend URL
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      //ValidateIssuerSigningKey = true,
                      //IssuerSigningKey = new SymmetricSecurityKey(key),
                      //ValidateIssuer = false,
                      //ValidateAudience = false,
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = jwtSettings["Issuer"],
                      ValidAudience = jwtSettings["Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(key)
                  };
              });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("manager"));
                options.AddPolicy("ExecutivePolicy", policy => policy.RequireRole("executive"));
                options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("customer"));
            });

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddControllers();
            builder.Services.Configure<REdbSettings>(
                    builder.Configuration.GetSection("REDataBaseConfig")
                );
            builder.Services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<REdbSettings>>().Value
            );
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<ICustomerRequestRepo, CustomerRequestRepo>();
            builder.Services.AddScoped<ICustomerRequestService, CustomerRequestService>();

            builder.Services.AddScoped<IPropertyRepo, PropertyRepo>();
            builder.Services.AddScoped<IPropertyService, PropertyService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCors("AllowSpecificOrigins");  // Use CORS
            app.MapControllers();
            app.Run();
        }
    }
}