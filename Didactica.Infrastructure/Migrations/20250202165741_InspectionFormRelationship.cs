using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Didactica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InspectionFormRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_inspection_forms_inspection_id",
                table: "inspection_forms");

            migrationBuilder.AddColumn<int>(
                name: "inspection_form_id",
                table: "inspections",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_inspection_forms_inspection_id",
                table: "inspection_forms",
                column: "inspection_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_inspection_forms_inspection_id",
                table: "inspection_forms");

            migrationBuilder.DropColumn(
                name: "inspection_form_id",
                table: "inspections");

            migrationBuilder.CreateIndex(
                name: "ix_inspection_forms_inspection_id",
                table: "inspection_forms",
                column: "inspection_id");
        }
    }
}
