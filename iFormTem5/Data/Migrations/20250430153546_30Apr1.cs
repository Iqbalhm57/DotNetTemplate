using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iFormTem5.Migrations
{
    /// <inheritdoc />
    public partial class _30Apr1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Templates",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_CreatedById",
                table: "Templates",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_AspNetUsers_CreatedById",
                table: "Templates",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_AspNetUsers_CreatedById",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Templates_CreatedById",
                table: "Templates");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Templates",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
