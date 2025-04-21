
using Consul;

namespace Hearth.CMicroservice
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

            IConfigurationSection cs = builder.Configuration.GetSection("Consul");

            var consulClient = new ConsulClient(x =>
            {
                // Consul �����ַ
                x.Address = new Uri(cs["Address"]);
            });
            var registration = new AgentServiceRegistration()
            {
                ID = Guid.NewGuid().ToString(),
                Name = cs["ServiceName"],// ������
                Address = cs["ServiceAddress"], // �����IP
                Port = int.Parse(cs["ServicePort"]), // ����󶨶˿�
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//����������ú�ע��
                    Interval = TimeSpan.FromSeconds(10),//�������ʱ����
                    HTTP = cs["HealthCheckAddress"],//��������ַ
                    Timeout = TimeSpan.FromSeconds(5)
                }
            };
            // ����ע��
            consulClient.Agent.ServiceRegister(registration).Wait();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Ӧ�ó�����ֹʱ������ȡ��ע��
            app.Lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
