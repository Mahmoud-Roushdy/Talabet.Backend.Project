using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Service.Extensions;
using Talabat.Service.Helpers;
using Talabat.Service.Mapping_Profiles;

namespace Talabat.Service
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
         
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbcontext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var Connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(Connection);
            });
            builder.Services.AddIdetntityServices(builder.Configuration);
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            //builder.Services.AddScoped<typeof(IBasketRepository),typeof(BasketRepository)>;
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            //builder.Services.AddScoped<IBasketRepository, BasketRepository>();

            builder.Services.AddScoped(typeof(IGenericRepositories<>), typeof(IGenericRepository<>));
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<PictureResolver>();
            

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
              var services = scope.ServiceProvider;
            var LoggerFactroy = services.GetRequiredService<ILoggerFactory>();
              try
                {
                var dbcontext = services.GetRequiredService<StoreDbcontext>();
                await dbcontext.Database.MigrateAsync(); 

                await DataSeeding.SeedingAsync(dbcontext);
                ////// 
                var UserMnager = services.GetRequiredService<UserManager<AppUser>>();
                var IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();
                await IdentityDbContextDataSeed.UserIdentitySeed(UserMnager);



                } 
               catch (Exception ex)
                { 
                  
                   var logger= LoggerFactroy.CreateLogger<Program>();
                logger.LogError(ex, " An error takes place within migration while updating database");
                }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run(); 
        }
    }
}
