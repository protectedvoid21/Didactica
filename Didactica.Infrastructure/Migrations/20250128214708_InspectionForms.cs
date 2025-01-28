using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Didactica.Api.Migrations
{
    /// <inheritdoc />
    public partial class InspectionForms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_inspections_inspection_methods_inspection_method_id",
                table: "inspections");

            migrationBuilder.DropTable(
                name: "inspection_methods");

            migrationBuilder.RenameColumn(
                name: "inspection_method_id",
                table: "inspections",
                newName: "inspection_team_id");

            migrationBuilder.RenameIndex(
                name: "ix_inspections_inspection_method_id",
                table: "inspections",
                newName: "ix_inspections_inspection_team_id");

            migrationBuilder.AlterColumn<string>(
                name: "justification",
                table: "appeals",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "inspection_forms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inspection_id = table.Column<int>(type: "integer", nullable: false),
                    appeal_id = table.Column<int>(type: "integer", nullable: true),
                    were_classes_on_time = table.Column<bool>(type: "boolean", nullable: false),
                    was_attendance_checked = table.Column<bool>(type: "boolean", nullable: false),
                    was_room_suitable = table.Column<bool>(type: "boolean", nullable: false),
                    presented_topic_and_scope = table.Column<int>(type: "integer", nullable: false),
                    explained_clearly = table.Column<int>(type: "integer", nullable: false),
                    was_engaged = table.Column<int>(type: "integer", nullable: false),
                    encouraged_independent_thinking = table.Column<int>(type: "integer", nullable: false),
                    maintained_documentation = table.Column<int>(type: "integer", nullable: false),
                    delivered_updated_knowledge = table.Column<int>(type: "integer", nullable: false),
                    presented_prepared_material = table.Column<int>(type: "integer", nullable: false),
                    final_grade_justification = table.Column<string>(type: "text", nullable: false),
                    conclusions_and_recommendations = table.Column<string>(type: "text", nullable: false),
                    final_grade = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inspection_forms", x => x.id);
                    table.ForeignKey(
                        name: "fk_inspection_forms_appeals_appeal_id",
                        column: x => x.appeal_id,
                        principalTable: "appeals",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_inspection_forms_inspections_inspection_id",
                        column: x => x.inspection_id,
                        principalTable: "inspections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inspection_teams",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inspection_teams", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inspection_team_teacher",
                columns: table => new
                {
                    inspection_teams_id = table.Column<int>(type: "integer", nullable: false),
                    teachers_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inspection_team_teacher", x => new { x.inspection_teams_id, x.teachers_id });
                    table.ForeignKey(
                        name: "fk_inspection_team_teacher_inspection_teams_inspection_teams_id",
                        column: x => x.inspection_teams_id,
                        principalTable: "inspection_teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_inspection_team_teacher_teachers_teachers_id",
                        column: x => x.teachers_id,
                        principalTable: "teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_inspection_forms_appeal_id",
                table: "inspection_forms",
                column: "appeal_id");

            migrationBuilder.CreateIndex(
                name: "ix_inspection_forms_inspection_id",
                table: "inspection_forms",
                column: "inspection_id");

            migrationBuilder.CreateIndex(
                name: "ix_inspection_team_teacher_teachers_id",
                table: "inspection_team_teacher",
                column: "teachers_id");

            migrationBuilder.AddForeignKey(
                name: "fk_inspections_inspection_teams_inspection_team_id",
                table: "inspections",
                column: "inspection_team_id",
                principalTable: "inspection_teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_inspections_inspection_teams_inspection_team_id",
                table: "inspections");

            migrationBuilder.DropTable(
                name: "inspection_forms");

            migrationBuilder.DropTable(
                name: "inspection_team_teacher");

            migrationBuilder.DropTable(
                name: "inspection_teams");

            migrationBuilder.RenameColumn(
                name: "inspection_team_id",
                table: "inspections",
                newName: "inspection_method_id");

            migrationBuilder.RenameIndex(
                name: "ix_inspections_inspection_team_id",
                table: "inspections",
                newName: "ix_inspections_inspection_method_id");

            migrationBuilder.AlterColumn<string>(
                name: "justification",
                table: "appeals",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "fk_inspections_inspection_methods_inspection_method_id",
                table: "inspections",
                column: "inspection_method_id",
                principalTable: "inspection_methods",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
