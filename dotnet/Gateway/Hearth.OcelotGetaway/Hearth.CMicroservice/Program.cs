
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
                // Consul 服务地址
                x.Address = new Uri(cs["Address"]);
            });
            var registration = new AgentServiceRegistration()
            {
                ID = Guid.NewGuid().ToString(),
                Name = cs["ServiceName"],// 服务名
                Address = cs["ServiceAddress"], // 服务绑定IP
                Port = int.Parse(cs["ServicePort"]), // 服务绑定端口
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                    Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔
                    HTTP = cs["HealthCheckAddress"],//健康检查地址
                    Timeout = TimeSpan.FromSeconds(5)
                }
            };
            // 服务注册
            consulClient.Agent.ServiceRegister(registration).Wait();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // 应用程序终止时，服务取消注册
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
