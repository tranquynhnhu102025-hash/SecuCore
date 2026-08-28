using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecuCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteTarget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteTarget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebsiteTarget_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScanLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    VulnerabilityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SeverityLevel = table.Column<int>(type: "int", nullable: false),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false),
                    ScanTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScanLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScanLog_WebsiteTarget_TargetId",
                        column: x => x.TargetId,
                        principalTable: "WebsiteTarget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash", "Role" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "admin@ntt.edu.vn", "Trần Quỳnh Như", "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9", "SystemAdmin" });

            migrationBuilder.CreateIndex(
                name: "IX_ScanLog_TargetId",
                table: "ScanLog",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteTarget_UserId",
                table: "WebsiteTarget",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScanLog");

            migrationBuilder.DropTable(
                name: "WebsiteTarget");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
