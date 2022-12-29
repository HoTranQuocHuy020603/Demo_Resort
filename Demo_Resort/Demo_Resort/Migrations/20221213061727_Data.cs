using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Demo_Resort.Migrations
{
    public partial class Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    isAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isEmployee = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    firstname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    lastname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    phonenumber = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    firstname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    lastname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    phonenumber = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    cid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    id = table.Column<int>(type: "int", nullable: false),
                    falname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    phonenumber = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    arrivaldate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    departuredate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    roomtype = table.Column<string>(type: "longtext", nullable: false),
                    totalprice = table.Column<int>(type: "int", nullable: false),
                    caterogy = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.cid);
                    table.ForeignKey(
                        name: "FK_Contracts_Employees_id",
                        column: x => x.id,
                        principalTable: "Employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_id",
                table: "Contracts",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
