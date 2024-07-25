using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class makeuniques : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsersAuths_email",
                table: "UsersAuths",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersAuths_phone_number",
                table: "UsersAuths",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersAuths_username",
                table: "UsersAuths",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersAuths_email",
                table: "UsersAuths");

            migrationBuilder.DropIndex(
                name: "IX_UsersAuths_phone_number",
                table: "UsersAuths");

            migrationBuilder.DropIndex(
                name: "IX_UsersAuths_username",
                table: "UsersAuths");
        }
    }
}
