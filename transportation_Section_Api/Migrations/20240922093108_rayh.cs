using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace transportation_Section_Api.Migrations
{
    /// <inheritdoc />
    public partial class rayh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageId);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ProviderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.ProviderId);
                });

            migrationBuilder.CreateTable(
                name: "TransportationTypes",
                columns: table => new
                {
                    TransportationTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationTypes", x => x.TransportationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Transportations",
                columns: table => new
                {
                    TransportationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportationTypeId = table.Column<int>(type: "int", nullable: false),
                    DepartureLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportations", x => x.TransportationId);
                    table.ForeignKey(
                        name: "FK_Transportations_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transportations_TransportationTypes_TransportationTypeId",
                        column: x => x.TransportationTypeId,
                        principalTable: "TransportationTypes",
                        principalColumn: "TransportationTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageTransportations",
                columns: table => new
                {
                    PackageTransportationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    TransportationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageTransportations", x => x.PackageTransportationId);
                    table.ForeignKey(
                        name: "FK_PackageTransportations_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageTransportations_Transportations_TransportationId",
                        column: x => x.TransportationId,
                        principalTable: "Transportations",
                        principalColumn: "TransportationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "PackageId", "Name" },
                values: new object[,]
                {
                    { 1, "Weekend Getaway" },
                    { 2, "Adventure Trip" }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "ProviderId", "Address", "CompanyName", "ContactNumber", "IsVerified", "Name" },
                values: new object[,]
                {
                    { 1, "123 Main St, Dhaka", "Company A", "123-456-7890", false, "Hanif" },
                    { 2, "456 Elm St, Rangamati", "Company B", "123-456-755", false, "Nabil" }
                });

            migrationBuilder.InsertData(
                table: "TransportationTypes",
                columns: new[] { "TransportationTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "Bus" },
                    { 2, "Train" },
                    { 3, "Airplane" }
                });

            migrationBuilder.InsertData(
                table: "Transportations",
                columns: new[] { "TransportationId", "ArrivalTime", "DepartureDate", "DepartureLocation", "Description", "IsActive", "ProviderId", "Rating", "TransportationTypeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 23, 17, 31, 7, 499, DateTimeKind.Local).AddTicks(776), new DateTime(2024, 9, 23, 15, 31, 7, 499, DateTimeKind.Local).AddTicks(757), "Dhaka", "Bus from Dhaka to RangaMati", true, 1, 5, 1 },
                    { 2, new DateTime(2024, 9, 24, 18, 31, 7, 499, DateTimeKind.Local).AddTicks(781), new DateTime(2024, 9, 24, 15, 31, 7, 499, DateTimeKind.Local).AddTicks(780), "Laxmipur", "Train from Laxmipur to Rangpur", true, 2, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "PackageTransportations",
                columns: new[] { "PackageTransportationId", "PackageId", "TransportationId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackageTransportations_PackageId",
                table: "PackageTransportations",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageTransportations_TransportationId",
                table: "PackageTransportations",
                column: "TransportationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_ProviderId",
                table: "Transportations",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_TransportationTypeId",
                table: "Transportations",
                column: "TransportationTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageTransportations");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Transportations");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "TransportationTypes");
        }
    }
}
