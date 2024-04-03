using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassIn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CheckInTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckIns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Attendee_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIns", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendee_Event_Id",
                table: "Attendee",
                column: "Event_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendee_Events_Event_Id",
                table: "Attendee",
                column: "Event_Id",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendee_Events_Event_Id",
                table: "Attendee");

            migrationBuilder.DropTable(
                name: "CheckIns");

            migrationBuilder.DropIndex(
                name: "IX_Attendee_Event_Id",
                table: "Attendee");
        }
    }
}
