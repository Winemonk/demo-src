
using BloggingApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestEFCore.Services;

namespace TestEFCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            var connectionString = builder.Configuration.GetConnectionString("BloggingDbConnection");
            builder.Services.AddDbContext<BloggingContext>(options =>
                options.UseNpgsql(connectionString));

            builder.Services.AddScoped<IBlogService, BlogService>();

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
        }
    }
}
