using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class CreateAspNetVoucherItemsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetVoucherItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    SubSidiaryId = table.Column<int>(type: "int", nullable: false),
                    ControlId = table.Column<int>(type: "int", nullable: false),
                    Debit = table.Column<double>(type: "float", nullable: false),
                    Credit = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetVoucherItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetVoucherItems_AspNetControls_ControlId",
                        column: x => x.ControlId,
                        principalTable: "AspNetControls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetVoucherItems_AspNetSubSidiaries_SubSidiaryId",
                        column: x => x.SubSidiaryId,
                        principalTable: "AspNetSubSidiaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AspNetVoucherItems_AspNetVouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "AspNetVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetVoucherItems_ControlId",
                table: "AspNetVoucherItems",
                column: "ControlId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetVoucherItems_SubSidiaryId",
                table: "AspNetVoucherItems",
                column: "SubSidiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetVoucherItems_VoucherId",
                table: "AspNetVoucherItems",
                column: "VoucherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetVoucherItems");
        }
    }
}
