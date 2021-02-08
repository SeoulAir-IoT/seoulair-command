using System;
using Microsoft.EntityFrameworkCore;
using SeoulAir.Command.Domain.Options;

namespace SeoulAir.Command.Repositories
{
    public class SeoulAirCommandDbContext : DbContext
    {
        public DbSet<Entities.Command> Commands { get; set; }

        public SeoulAirCommandDbContext(DbContextOptions<SeoulAirCommandDbContext> options)
            : base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var seoulairDeviceOptons = new MicroserviceUrlOptions
            {
                Address = "seoulair-device",
                Port = 5500
            };
            var seoulairDataOptions = new MicroserviceUrlOptions
            {
                Address = "seoulair-data",
                Port = 5600
            };

            modelBuilder.Entity<Entities.Command>().HasData(new Entities.Command
            {
                Id = Guid.NewGuid(),
                Address = seoulairDeviceOptons.Address,
                Port = seoulairDeviceOptons.Port,
                Controller = "api/AirQualitySensor",
                Endpoint = "TurnOn",
                Name = "sensor-on",
                NumOfParameters = 0,
                HttpMethod = "PUT"
            });
            
            modelBuilder.Entity<Entities.Command>().HasData(new Entities.Command
            {
                Id = Guid.NewGuid(),
                Address = seoulairDeviceOptons.Address,
                Port = seoulairDeviceOptons.Port,
                Controller = "api/AirQualitySensor",
                Endpoint = "TurnOff",
                Name = "sensor-off",
                NumOfParameters = 0,
                HttpMethod = "PUT"
            });
            
            modelBuilder.Entity<Entities.Command>().HasData(new Entities.Command
            {
                Id = Guid.NewGuid(),
                Address = seoulairDeviceOptons.Address,
                Port = seoulairDeviceOptons.Port,
                Controller = "api/AirQualitySensor",
                Endpoint = "IsOn",
                Name = "sensor-status",
                NumOfParameters = 0,
                HttpMethod = "GET"
            });
            
            modelBuilder.Entity<Entities.Command>().HasData(new Entities.Command
            {
                Id = Guid.NewGuid(),
                Address = seoulairDeviceOptons.Address,
                Port = seoulairDeviceOptons.Port,
                Controller = "api/SignalLight",
                Endpoint = "IsOn",
                Name = "signal-light-status",
                NumOfParameters = 1,
                HttpMethod = "GET"
            });
            
            modelBuilder.Entity<Entities.Command>().HasData(new Entities.Command
            {
                Id = Guid.NewGuid(),
                Address = seoulairDeviceOptons.Address,
                Port = seoulairDeviceOptons.Port,
                Controller = "api/SignalLight",
                Endpoint = "TurnOn",
                Name = "signal-light-on",
                NumOfParameters = 1,
                HttpMethod = "PUT"
            });
            
            modelBuilder.Entity<Entities.Command>().HasData(new Entities.Command
            {
                Id = Guid.NewGuid(),
                Address = seoulairDeviceOptons.Address,
                Port = seoulairDeviceOptons.Port,
                Controller = "api/SignalLight",
                Endpoint = "TurnOff",
                Name = "signal-light-off",
                NumOfParameters = 1,
                HttpMethod = "PUT"
            });
            
            modelBuilder.Entity<Entities.Command>().HasData(new Entities.Command
            {
                Id = Guid.NewGuid(),
                Address = seoulairDeviceOptons.Address,
                Port = seoulairDeviceOptons.Port,
                Controller = "api/SignalLight",
                Endpoint = "ActiveColor",
                Name = "change-light-color",
                NumOfParameters = 2,
                HttpMethod = "PUT"
            });

            modelBuilder.Entity<Entities.Command>().HasData(new Entities.Command
            {
                Id = Guid.NewGuid(),
                Address = seoulairDataOptions.Address,
                Port = seoulairDataOptions.Port,
                Controller = "api/Actuator",
                Endpoint = "IsOn",
                Name = "data-status",
                NumOfParameters = 0,
                HttpMethod = "GET"
            });
            
            modelBuilder.Entity<Entities.Command>().HasData(new Entities.Command
            {
                Id = Guid.NewGuid(),
                Address = seoulairDataOptions.Address,
                Port = seoulairDataOptions.Port,
                Controller = "api/Actuator",
                Endpoint = "TurnOn",
                Name = "data-on",
                NumOfParameters = 0,
                HttpMethod = "PUT"
            });
            
            modelBuilder.Entity<Entities.Command>().HasData(new Entities.Command
            {
                Id = Guid.NewGuid(),
                Address = seoulairDataOptions.Address,
                Port = seoulairDataOptions.Port,
                Controller = "api/Actuator",
                Endpoint = "TurnOff",
                Name = "data-off",
                NumOfParameters = 0,
                HttpMethod = "PUT"
            });
        }
    }
}
