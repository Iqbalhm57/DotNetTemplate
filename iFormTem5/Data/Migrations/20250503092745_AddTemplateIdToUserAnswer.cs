using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iFormTem5.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplateIdToUserAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "UserAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_TemplateId",
                table: "UserAnswers",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Templates_TemplateId",
                table: "UserAnswers",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Templates_TemplateId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_TemplateId",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "UserAnswers");
        }
    }
}
