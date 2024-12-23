using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Didactica.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameHospitationToInspection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hospitations");

            migrationBuilder.DropTable(
                name: "hospitation_methods");

            migrationBuilder.RenameColumn(
                name: "lesson_name",
                table: "lessons",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "lesson_date",
                table: "lessons",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "lesson_code",
                table: "lessons",
                newName: "code");

            migrationBuilder.CreateTable(
                name: "inspection_methods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inspection_methods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inspections",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inspection_method_id = table.Column<int>(type: "integer", nullable: false),
                    employee_id = table.Column<int>(type: "integer", nullable: false),
                    lesson_id = table.Column<int>(type: "integer", nullable: false),
                    is_remote = table.Column<bool>(type: "boolean", nullable: false),
                    lesson_environment = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inspections", x => x.id);
                    table.ForeignKey(
                        name: "fk_inspections_employees_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_inspections_inspection_methods_inspection_method_id",
                        column: x => x.inspection_method_id,
                        principalTable: "inspection_methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_inspections_lessons_lesson_id",
                        column: x => x.lesson_id,
                        principalTable: "lessons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_inspections_employee_id",
                table: "inspections",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "ix_inspections_inspection_method_id",
                table: "inspections",
                column: "inspection_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_inspections_lesson_id",
                table: "inspections",
                column: "lesson_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inspections");

            migrationBuilder.DropTable(
                name: "inspection_methods");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "lessons",
                newName: "lesson_name");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "lessons",
                newName: "lesson_date");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "lessons",
                newName: "lesson_code");

            migrationBuilder.CreateTable(
                name: "hospitation_methods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hospitation_methods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hospitations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hospitation_method_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    hospitation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hospitations", x => x.id);
                    table.ForeignKey(
                        name: "fk_hospitations_hospitation_methods_hospitation_method_id",
                        column: x => x.hospitation_method_id,
                        principalTable: "hospitation_methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_hospitations_hospitation_method_id",
                table: "hospitations",
                column: "hospitation_method_id");
        }
    }
}
