using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppraisalManagentSystem.Migrations
{
    public partial class wkanjakfcdassaaskasSDV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Competency",
                table: "AppraisalStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Competency",
                table: "AppraisalStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
