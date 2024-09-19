using DotNetBackend.Models;
using DotNetBackend.Repositories;
using DotNetBackend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DotNetBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust the session timeout as needed
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.None; // Enable cross-origin cookies
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure the cookie is sent over HTTPS
            });
            builder.Services.AddAuthentication("Cookies")
                .AddCookie(options =>
                {
                    options.LoginPath = "/customers/login";  // Define your login path
                    options.LogoutPath = "/customers/logout";
                    options.LoginPath = "/users/login";  // Define your login path
                    options.LogoutPath = "/users/logout";
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
            app.UseSession();
            app.UseAuthorization();
            app.UseCors("AllowSpecificOrigins");  // Use CORS
            app.MapControllers();
            app.Run();
        }
    }
}