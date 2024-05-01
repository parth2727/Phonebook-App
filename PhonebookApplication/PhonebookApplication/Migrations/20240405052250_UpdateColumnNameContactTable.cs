using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhonebookApplication.Migrations
{
    public partial class UpdateColumnNameContactTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comapany",
                table: "Contacts",
                newName: "Company");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Company",
                table: "Contacts",
                newName: "Comapany");
        }
    }
}
