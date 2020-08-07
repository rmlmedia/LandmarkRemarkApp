using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TigerSpike.LandmarkRemark.Data.Migrations
{
    public partial class TestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Fullname", "Created" },
                values: new object[] { "rick.lannan", "Rick Lannan", DateTime.Now });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Fullname", "Created" },
                values: new object[] { "matthew.whelan", "Matthew Whelan", DateTime.Now });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Fullname", "Created" },
                values: new object[] { "adrian.roney", "Adrian Roney", DateTime.Now });

            migrationBuilder.InsertData(
                table: "Landmarks",
                columns: new[] { "Username", "Latitude", "Longitude", "Comment", "Created" },
                values: new object[] { "rick.lannan", -37.811120, 145.007940, "This is where I live", DateTime.Now });

            migrationBuilder.InsertData(
                table: "Landmarks",
                columns: new[] { "Username", "Latitude", "Longitude", "Comment", "Created" },
                values: new object[] { "rick.lannan", -37.813940, 144.973950, "This is TigerSpike HQ in Melbourne", DateTime.Now });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users", true);
            migrationBuilder.Sql("DELETE FROM Landmarks", true);
        }
    }
}
