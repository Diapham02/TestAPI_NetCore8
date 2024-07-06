using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_API.Migrations
{
    /// <inheritdoc />
    public partial class FinalUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KHOA",
                columns: table => new
                {
                    KhoaId = table.Column<int>(type: "int", nullable: false),
                    MaKhoa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenKhoa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KHOA", x => x.KhoaId);
                });

            migrationBuilder.CreateTable(
                name: "SINH_VIEN",
                columns: table => new
                {
                    MaSV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSV = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SINH_VIEN", x => x.MaSV);
                    table.ForeignKey(
                        name: "FK_SINH_VIEN_KHOA_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "KHOA",
                        principalColumn: "KhoaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SINH_VIEN_KhoaId",
                table: "SINH_VIEN",
                column: "KhoaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SINH_VIEN");

            migrationBuilder.DropTable(
                name: "KHOA");
        }
    }
}
