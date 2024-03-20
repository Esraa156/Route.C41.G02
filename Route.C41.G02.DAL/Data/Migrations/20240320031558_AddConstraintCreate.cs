using Microsoft.EntityFrameworkCore.Migrations;

namespace Route.C41.G02.DAL.Data.Migrations
{
    public partial class AddConstraintCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                type: "varchar(22)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Departments",
                type: "varchar(22)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(22)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Departments",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(22)");
        }
    }
}
