
using Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hearth.TestGwAService
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

            var key = Encoding.UTF8.GetBytes("12345678901234561234567890123456");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "Hearth.AuthService",
                        ValidAudience = "Hearth.OcelotGw",
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            var consulClient = new ConsulClient(x =>
            {
                // consul �����ַ
                x.Address = new Uri("http://localhost:8500");
            });
            var registration = new AgentServiceRegistration()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "ServiceA",// ������
                Address = "localhost", // �����IP
                Port = 5001, // ����󶨶˿�
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//����������ú�ע��
                    Interval = TimeSpan.FromSeconds(10),//�������ʱ����
                    HTTP = "http://localhost:5001/healthCheck",//��������ַ
                    Timeout = TimeSpan.FromSeconds(5)
                }
            };
            // ����ע��
            consulClient.Agent.ServiceRegister(registration).Wait();

            var app = builder.Build();

            // Ӧ�ó�����ֹʱ������ȡ��ע��
            app.Lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
