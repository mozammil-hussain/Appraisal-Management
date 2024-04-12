using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppraisalManagentSystem.Migrations
{
    public partial class dsasadhsfdaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appraisals",
                columns: table => new
                {
                    _detailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _appraisalId = table.Column<int>(type: "int", nullable: false),
                    _competency = table.Column<int>(type: "int", nullable: false),
                    _emp_rating = table.Column<int>(type: "int", nullable: false),
                    _emp_comments = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    _manager_rating = table.Column<int>(type: "int", nullable: false),
                    _manager_comments = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appraisals", x => x._detailsId);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalStatus",
                columns: table => new
                {
                    AppraisalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Objective = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalStatus", x => x.AppraisalId);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalWithCopms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppraisalId = table.Column<int>(type: "int", nullable: false),
                    Compitency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalWithCopms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competencies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeC = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competencies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _emp_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _emp_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _emp_password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _emp_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _managerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appraisals");

            migrationBuilder.DropTable(
                name: "AppraisalStatus");

            migrationBuilder.DropTable(
                name: "AppraisalWithCopms");

            migrationBuilder.DropTable(
                name: "Competencies");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "LoginDetails");
        }
    }
}
