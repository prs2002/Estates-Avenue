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
            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);

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

            //builder.Services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust the session timeout as needed
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //    options.Cookie.SameSite = SameSiteMode.None; // Enable cross-origin cookies
            //   options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure the cookie is sent over HTTPS
            //});
            //  builder.Services.AddAuthentication("Cookies")
            //      .AddCookie(options =>
            //      {
            //          options.LoginPath = "/users/login";  // Define your login path
            //          options.LogoutPath = "/users/logout";
            //      }); 
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
                      ValidIssuer = jwtSettings["Jwt:Issuer"],
                      ValidAudience = jwtSettings["Jwt:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(key)
                  };
              });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
                options.AddPolicy("ExecutivePolicy", policy => policy.RequireRole("Executive"));
                options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
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
            builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
            builder.Services.AddScoped<ICustomerService, CustomerService>(); 
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IExecutiveRepo, ExecutiveRepo>();
            builder.Services.AddScoped<IExecutiveService, ExecutiveService>();
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
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCors("AllowSpecificOrigins");  // Use CORS
            app.MapControllers();
            app.Run();
        }
    }
}