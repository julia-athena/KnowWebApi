using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixShema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TagId",
                table: "Tags",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(124)");

            migrationBuilder.AlterColumn<string>(
                name: "TagsTagId",
                table: "PostTag",
                type: "character varying(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(124)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Posts",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "Черновик",
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldDefaultValue: "Черновик");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TagId",
                table: "Tags",
                type: "varchar(124)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "TagsTagId",
                table: "PostTag",
                type: "varchar(124)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "varchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Posts",
                type: "varchar(64)",
                nullable: false,
                defaultValue: "Черновик",
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128,
                oldDefaultValue: "Черновик");
        }
    }
}
