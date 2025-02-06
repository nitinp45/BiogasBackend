using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biogas_BackendEF.Migrations
{
    /// <inheritdoc />
    public partial class BiogasAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", nullable: false),
                    Role = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UId);
                });

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    ProducerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductionCapacity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.ProducerId);
                    table.ForeignKey(
                        name: "FK_Producers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BiogasInventory",
                columns: table => new
                {
                    BiogasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiogasInventory", x => x.BiogasId);
                    table.ForeignKey(
                        name: "FK_BiogasInventory_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "ProducerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WasteContributor",
                columns: table => new
                {
                    ContributorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WasteType = table.Column<string>(type: "varchar(50)", nullable: false),
                    WasteQuantity = table.Column<int>(type: "int", nullable: false),
                    CollectedBy = table.Column<int>(type: "int", nullable: true),
                    ContributionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteContributor", x => x.ContributorId);
                    table.ForeignKey(
                        name: "FK_WasteContributor_Producers_CollectedBy",
                        column: x => x.CollectedBy,
                        principalTable: "Producers",
                        principalColumn: "ProducerId");
                    table.ForeignKey(
                        name: "FK_WasteContributor_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UId = table.Column<int>(type: "int", nullable: false),
                    BiogasId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    TransactionId = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_BiogasInventory_BiogasId",
                        column: x => x.BiogasId,
                        principalTable: "BiogasInventory",
                        principalColumn: "BiogasId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UId",
                        column: x => x.UId,
                        principalTable: "Users",
                        principalColumn: "UId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BiogasInventory_ProducerId",
                table: "BiogasInventory",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BiogasId",
                table: "Orders",
                column: "BiogasId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UId",
                table: "Orders",
                column: "UId");

            migrationBuilder.CreateIndex(
                name: "IX_Producers_UserId",
                table: "Producers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteContributor_CollectedBy",
                table: "WasteContributor",
                column: "CollectedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WasteContributor_UserId",
                table: "WasteContributor",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "WasteContributor");

            migrationBuilder.DropTable(
                name: "BiogasInventory");

            migrationBuilder.DropTable(
                name: "Producers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
