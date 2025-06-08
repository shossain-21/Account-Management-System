using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class CreateAspNetSubSidiariesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetSubSidiaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ControlId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningDr = table.Column<double>(type: "float", nullable: false),
                    OpeningCr = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetSubSidiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetSubSidiaries_AspNetControls_ControlId",
                        column: x => x.ControlId,
                        principalTable: "AspNetControls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetSubSidiaries_ControlId",
                table: "AspNetSubSidiaries",
                column: "ControlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetSubSidiaries");
        }
    }
}
