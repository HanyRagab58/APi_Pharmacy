using Api.Extenstions;
using Api.Helpers;
using Api.Middleware;
using AutoMapper;
using Demo.BLL.Inerfaces;
using Demo.DAL;
using Demo.DAL.Data;
using Demo.DAL.Identity;
using Demo.DAL.Inerfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using StackExchange.Redis;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddScoped<IProductRepository, ProductRepository>();
            //builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            //builder.Services.AddAutoMapper(typeof(MappingProfilles));
            builder.Services.AddAplicationServic();
            builder.Services.AddIdentityService(builder.Configuration);
            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            }); 
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwggerDocumentation();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseMiddleware<ExeptionMiddleware>();
            }
            Seed.SeedData(app);
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            //async void SeedData()
            //{
            //    using (var scop = app.Services.CreateScope())
            //    {
            //        var services = scop.ServiceProvider;
            //        var loggerFactory =services.GetRequiredService<ILoggerFactory>();
            //        try
            //        {
            //            var context = services.GetRequiredService<StoreContext>();

            //            await context.Database.MigrateAsync();
            //            await StoreContextSeed.SeedAsync(context ,loggerFactory);
            //        }
            //        catch (Exception ex)
            //        {

            //            var logger = loggerFactory.CreateLogger<Program>();
            //            logger.LogError(ex.Message);
            //        }
            //    }

            //}
        }
    }
}