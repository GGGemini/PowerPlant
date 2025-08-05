using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PowerPlant.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergyBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StationId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NextServiceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SensorsCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergyBlocks_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Станция 1" },
                    { 2, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Станция 2" }
                });

            migrationBuilder.InsertData(
                table: "EnergyBlocks",
                columns: new[] { "Id", "CreatedAt", "Name", "NextServiceDate", "SensorsCount", "StationId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Энергоблок 1.1", new DateOnly(2025, 12, 1), 42, 1 },
                    { 2, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Энергоблок 1.2", new DateOnly(2026, 4, 3), 35, 1 },
                    { 3, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Энергоблок 2.1", new DateOnly(2026, 1, 5), 50, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "PasswordHash", "Patronymic", "RoleId", "Surname" },
                values: new object[,]
                {
                    { 1, "tanchikipro7777777@gmail.com", "Админ", "$2a$11$NSQdN6n7l1b0q6wOyJ/zDO0sVuED8h2VD8wJEDTSX51ryd8l7OKTS", null, 1, "Главный" },
                    { 2, "cheburashka@gmail.com", "Пользователь", "$2a$11$s22XKQRuh81CSJPkU9TY6eVCR8GtcuAGeCHeWvFVfs9fNTTl6n8Xe", null, 2, "Простой" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnergyBlocks_StationId",
                table: "EnergyBlocks",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnergyBlocks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
