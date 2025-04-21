
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Values;
using System.Text;

namespace Hearth.OcelotGetaway
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

            const string authenticationProviderKey = "hearthAuth";
            const string authenticationProviderKey2 = "hearthAuth2";
            builder.Services.AddAuthentication(authenticationProviderKey)
                .AddJwtBearer(authenticationProviderKey, option =>
                {
                    option.Authority = "http://localhost:7000";
                    option.RequireHttpsMetadata = false;
                })
                .AddJwtBearer(authenticationProviderKey2, option =>
                {
                    option.Authority = "http://localhost:7000";
                    option.RequireHttpsMetadata = false;
                });

            //const string authenticationProviderKey = "Bearer";
            //builder.Services.AddAuthentication()
            //    .AddJwtBearer(authenticationProviderKey, options =>
            //    {
            //        options.RequireHttpsMetadata = false;
            //        options.Authority = "https://localhost:7000/api/Auth";
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidIssuer = "hearth", // 认证服务的 Issuer
            //            ValidAudience = "hearth", // 认证服务的 Audience
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("12345678901234561234567890123456")) // 密钥
            //        };
            //    });

            builder.Configuration.SetBasePath(AppContext.BaseDirectory).AddJsonFile("ocelot.json");
            builder.Services.AddOcelot().AddConsul();

            var app = builder.Build();

            app.UseOcelot().Wait();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
