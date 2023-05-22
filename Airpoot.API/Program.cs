
using Airpoot.API.BL;
using Airpoot.API.DAL;
using Airpoot.API.HUB;
using Airpoot.API.HUB.Clients;
using Microsoft.EntityFrameworkCore;

namespace Airpoot.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AirportHistoryContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MainConnection")),ServiceLifetime.Singleton);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSignalR();


            builder.Services.AddSingleton<IRepository<AirplaneHistory>, HistoryRepository>();
            builder.Services.AddSingleton<AirportLogic>();
            builder.Services.AddSingleton<BL.RouteProvider>();
            //builder.Services.AddSingleton<Simulator.Simulator>();

            builder.Services.AddSingleton<MyTimer>();

            builder.Services.AddCors(o => o.AddDefaultPolicy(
                o => o.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                ));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseAuthorization();


            app.MapControllers();

            app.MapHub<AirportHub>("/AirportHub");

            app.Run();
        }
    }
}