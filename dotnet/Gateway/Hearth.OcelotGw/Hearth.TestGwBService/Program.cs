
using Consul;

namespace Hearth.TestGwBService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at http://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var consulClient = new ConsulClient(x =>
            {
                // consul �����ַ
                x.Address = new Uri("http://localhost:8500");
            });
            var registration = new AgentServiceRegistration()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "ServiceB",// ������
                Address = "localhost", // �����IP
                Port = 5002, // ����󶨶˿�
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//����������ú�ע��
                    Interval = TimeSpan.FromSeconds(10),//�������ʱ����
                    HTTP = "http://localhost:5002/healthCheck",//��������ַ
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
