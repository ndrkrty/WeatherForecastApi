using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherApi.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherForecast",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyHighestTemp = table.Column<float>(type: "real", nullable: false),
                    DailyLowestTemp = table.Column<float>(type: "real", nullable: false),
                    WeeklyHighestTemp = table.Column<float>(type: "real", nullable: false),
                    WeeklyLowestTemp = table.Column<float>(type: "real", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecast", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecast");
        }
    }
}
