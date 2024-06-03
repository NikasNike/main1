using DataBase;
using Microsoft.EntityFrameworkCore;
using Seminar3Application.Abstractions;
using Seminar3Application.Queries;
using Seminar3Application.Services;
using MapperProfile = DataBase.Repositories.MappingSettings.MappingProfile;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddMemoryCache();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IStorageService, StorageService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IStorageFilling, StorageFilling>();

 builder.Host.UseServiceProviderFactory(new Autofac.Extensions.DependencyInjection.AutofacServiceProviderFactory());
 builder.Host.ConfigureContainer<Autofac.ContainerBuilder>(cb =>
{
     cb.Register(c => new AppDbContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency();
 });

builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        options.UseLazyLoadingProxies();
    }
);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<MySimpleQuery>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();