using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassIn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class passwordAttendee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Attendee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_Attendee_Id",
                table: "CheckIns",
                column: "Attendee_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Attendee_Attendee_Id",
                table: "CheckIns",
                column: "Attendee_Id",
                principalTable: "Attendee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Attendee_Attendee_Id",
                table: "CheckIns");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_Attendee_Id",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Attendee");
        }
    }
}
