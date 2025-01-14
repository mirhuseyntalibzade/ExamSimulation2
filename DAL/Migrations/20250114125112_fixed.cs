using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class @fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Technicians_TechnicianId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_TechnicianId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "TechnicianId",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Technicians",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_ServiceId",
                table: "Technicians",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technicians_Services_ServiceId",
                table: "Technicians",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technicians_Services_ServiceId",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_ServiceId",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Technicians");

            migrationBuilder.AddColumn<int>(
                name: "TechnicianId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Services_TechnicianId",
                table: "Services",
                column: "TechnicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Technicians_TechnicianId",
                table: "Services",
                column: "TechnicianId",
                principalTable: "Technicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
