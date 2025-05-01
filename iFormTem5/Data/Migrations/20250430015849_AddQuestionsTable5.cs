using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iFormTem5.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionsTable5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemplateTags_Templates_TemplateId1",
                table: "TemplateTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemplateTags",
                table: "TemplateTags");

            migrationBuilder.DropIndex(
                name: "IX_TemplateTags_TemplateId1",
                table: "TemplateTags");

            migrationBuilder.RenameColumn(
                name: "TemplateId1",
                table: "TemplateTags",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateId",
                table: "TemplateTags",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TemplateTags",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemplateTags",
                table: "TemplateTags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateTags_TemplateId",
                table: "TemplateTags",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateTags_Templates_TemplateId",
                table: "TemplateTags",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemplateTags_Templates_TemplateId",
                table: "TemplateTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemplateTags",
                table: "TemplateTags");

            migrationBuilder.DropIndex(
                name: "IX_TemplateTags_TemplateId",
                table: "TemplateTags");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TemplateTags",
                newName: "TemplateId1");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateId",
                table: "TemplateTags",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TemplateId1",
                table: "TemplateTags",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemplateTags",
                table: "TemplateTags",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateTags_TemplateId1",
                table: "TemplateTags",
                column: "TemplateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateTags_Templates_TemplateId1",
                table: "TemplateTags",
                column: "TemplateId1",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
