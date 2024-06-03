using DataBase;
using DataBase.Repositories.Abstraction;
using DataBase.Repositories.MappingSettings;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataBase.Data;
using DataBase.Repositories;

public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services) {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddMemoryCache(options => options.TrackStatistics = true);

        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            options.UseLazyLoadingProxies();
        });

        services.AddScoped<IDbInitializer, EfDbInitializer>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer) {
        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else {
            app.UseHttpsRedirection();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
            var dbContext = serviceScope.ServiceProvider.GetService<DataContext>();
            if (!dbContext.Database.CanConnect()) {
                dbContext.Database.EnsureCreated();
            }
            else if (!dbContext.Products.Any() || !dbContext.Storages.Any()) {
                dbInitializer.InitializeDb();
                Console.WriteLine(
                    "В базе данных нет записей. добавте парочку.");
            }
        }
    }
}