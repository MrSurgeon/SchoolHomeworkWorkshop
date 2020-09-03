using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompany.School.HomeworkDemo.Data.Migrations.SchoolDataDb
{
    public partial class AddTableHomework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Homeworks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeworkDescription = table.Column<string>(nullable: true),
                    LoadDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.Id);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Homeworks");

        }
    }
}
