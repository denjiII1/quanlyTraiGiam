using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrisonApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CanBos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhongPhuTrach = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanBos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HoSos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhongGiams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiPhong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SucChua = table.Column<int>(type: "int", nullable: false),
                    KhuGiam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongGiams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quyens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quyens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhamNhans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TomTatToiPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MucAn = table.Column<int>(type: "int", nullable: false),
                    NgayVaoTrai = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhongGiamId = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhamNhans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhamNhans_PhongGiams_PhongGiamId",
                        column: x => x.PhongGiamId,
                        principalTable: "PhongGiams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhauHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuyenId = table.Column<int>(type: "int", nullable: false),
                    HoSoId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.Ten);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_HoSos_HoSoId",
                        column: x => x.HoSoId,
                        principalTable: "HoSos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaiKhoans_Quyens_QuyenId",
                        column: x => x.QuyenId,
                        principalTable: "Quyens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhenThuongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhamNhanId = table.Column<int>(type: "int", nullable: false),
                    NgayKhenThuong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhenThuongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhenThuongs_PhamNhans_PhamNhanId",
                        column: x => x.PhamNhanId,
                        principalTable: "PhamNhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KyLuats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhamNhanId = table.Column<int>(type: "int", nullable: false),
                    NgayKyLuat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HinhThuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KyLuats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KyLuats_PhamNhans_PhamNhanId",
                        column: x => x.PhamNhanId,
                        principalTable: "PhamNhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichSinhHoats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhamNhanId = table.Column<int>(type: "int", nullable: false),
                    Ngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoatDong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSinhHoats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichSinhHoats_PhamNhans_PhamNhanId",
                        column: x => x.PhamNhanId,
                        principalTable: "PhamNhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichSuTruyCaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiKhoanTen = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChiIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuTruyCaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichSuTruyCaps_TaiKhoans_TaiKhoanTen",
                        column: x => x.TaiKhoanTen,
                        principalTable: "TaiKhoans",
                        principalColumn: "Ten",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Quyens",
                columns: new[] { "Id", "MoTa", "Ten" },
                values: new object[,]
                {
                    { 1, "Quản trị hệ thống", "Admin" },
                    { 2, "Cán bộ quản lý", "QuanLy" },
                    { 3, "Nhân viên trại", "NhanVien" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_KhenThuongs_PhamNhanId",
                table: "KhenThuongs",
                column: "PhamNhanId");

            migrationBuilder.CreateIndex(
                name: "IX_KyLuats_PhamNhanId",
                table: "KyLuats",
                column: "PhamNhanId");

            migrationBuilder.CreateIndex(
                name: "IX_LichSinhHoats_PhamNhanId",
                table: "LichSinhHoats",
                column: "PhamNhanId");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuTruyCaps_TaiKhoanTen",
                table: "LichSuTruyCaps",
                column: "TaiKhoanTen");

            migrationBuilder.CreateIndex(
                name: "IX_PhamNhans_PhongGiamId",
                table: "PhamNhans",
                column: "PhongGiamId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_HoSoId",
                table: "TaiKhoans",
                column: "HoSoId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_QuyenId",
                table: "TaiKhoans",
                column: "QuyenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanBos");

            migrationBuilder.DropTable(
                name: "KhenThuongs");

            migrationBuilder.DropTable(
                name: "KyLuats");

            migrationBuilder.DropTable(
                name: "LichSinhHoats");

            migrationBuilder.DropTable(
                name: "LichSuTruyCaps");

            migrationBuilder.DropTable(
                name: "PhamNhans");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "PhongGiams");

            migrationBuilder.DropTable(
                name: "HoSos");

            migrationBuilder.DropTable(
                name: "Quyens");
        }
    }
}
