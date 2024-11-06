using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HostingProject.Migrations
{
    /// <inheritdoc />
    public partial class serviceMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "service",
                columns: table => new
                {
                    sId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    planId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUntil = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service", x => x.sId);
                    table.ForeignKey(
                        name: "FK_service_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_service_Plans_planId",
                        column: x => x.planId,
                        principalTable: "Plans",
                        principalColumn: "planId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_service_planId",
                table: "service",
                column: "planId");

            migrationBuilder.CreateIndex(
                name: "IX_service_userId",
                table: "service",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "service");
        }
    }
}
