using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataBase.Repositories;
using DataBase.Repositories.Abstraction;

namespace WebApplication1 {
    public class Program {
        public static void Main(string[] args) {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder => {
                    builder.RegisterType<ProductRepository>().As<IProductRepository>();
                });
        
    }
}