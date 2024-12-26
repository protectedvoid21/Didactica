using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Didactica.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameEmployeeToTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_inspections_employees_employee_id",
                table: "inspections");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropColumn(
                name: "date",
                table: "inspections");

            migrationBuilder.RenameColumn(
                name: "specialization_name",
                table: "specializations",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "employee_id",
                table: "inspections",
                newName: "teacher_id");

            migrationBuilder.RenameIndex(
                name: "ix_inspections_employee_id",
                table: "inspections",
                newName: "ix_inspections_teacher_id");

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    faculty = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teachers", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "fk_inspections_teachers_teacher_id",
                table: "inspections",
                column: "teacher_id",
                principalTable: "teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_inspections_teachers_teacher_id",
                table: "inspections");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "specializations",
                newName: "specialization_name");

            migrationBuilder.RenameColumn(
                name: "teacher_id",
                table: "inspections",
                newName: "employee_id");

            migrationBuilder.RenameIndex(
                name: "ix_inspections_teacher_id",
                table: "inspections",
                newName: "ix_inspections_employee_id");

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "inspections",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    faculty = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    surname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "fk_inspections_employees_employee_id",
                table: "inspections",
                column: "employee_id",
                principalTable: "employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
