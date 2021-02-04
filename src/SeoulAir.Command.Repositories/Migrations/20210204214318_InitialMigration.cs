using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SeoulAir.Command.Repositories.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Port = table.Column<int>(nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Endpoint = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    NumOfParameters = table.Column<int>(nullable: false),
                    HttpMethod = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "Address", "Controller", "Endpoint", "HttpMethod", "Name", "NumOfParameters", "Port" },
                values: new object[,]
                {
                    { new Guid("f0181c30-b016-4918-abbc-a55578224186"), "seoulair-device", "AirQualitySensor", "TurnOn", "PUT", "sensor-on", 0, 5500 },
                    { new Guid("4f505368-6e4b-4421-a08e-6e7f5af9cd8f"), "seoulair-device", "AirQualitySensor", "TurnOff", "PUT", "sensor-off", 0, 5500 },
                    { new Guid("5b4dec30-8fae-457b-8fbe-1f22cea79f2a"), "seoulair-device", "AirQualitySensor", "IsOn", "GET", "sensor-status", 0, 5500 },
                    { new Guid("077ee73c-83f1-410d-b684-8faa0f2f95fe"), "seoulair-device", "SignalLight", "IsOn", "GET", "signal-light-status", 1, 5500 },
                    { new Guid("628f6831-27ce-4f78-8074-e5f5f84b2077"), "seoulair-device", "SignalLight", "TurnOn", "PUT", "signal-light-on", 1, 5500 },
                    { new Guid("5ed2ea37-111b-47d6-95a8-dcda5353f398"), "seoulair-device", "SignalLight", "TurnOff", "PUT", "signal-light-off", 1, 5500 },
                    { new Guid("b256af9b-0b1d-4739-8c62-be90465fb7d5"), "seoulair-device", "SignalLight", "ActiveColor", "PUT", "change-light-color", 2, 5500 },
                    { new Guid("b6cef9a9-ccd2-455a-a4be-6c987671af22"), "seoulair-data", "Actuator", "IsOn", "GET", "data-status", 0, 5600 },
                    { new Guid("d6a05a61-b36b-4780-b611-2b7bba0c33b8"), "seoulair-data", "Actuator", "TurnOn", "PUT", "data-on", 0, 5600 },
                    { new Guid("fa390861-00ba-4783-b94a-b9b84ffffad1"), "seoulair-data", "Actuator", "TurnOff", "PUT", "data-off", 0, 5600 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commands");
        }
    }
}
