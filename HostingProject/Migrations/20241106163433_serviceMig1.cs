using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HostingProject.Migrations
{
    /// <inheritdoc />
    public partial class serviceMig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_service_AspNetUsers_userId",
                table: "service");

            migrationBuilder.DropForeignKey(
                name: "FK_service_Plans_planId",
                table: "service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_service",
                table: "service");

            migrationBuilder.RenameTable(
                name: "service",
                newName: "userServices");

            migrationBuilder.RenameIndex(
                name: "IX_service_userId",
                table: "userServices",
                newName: "IX_userServices_userId");

            migrationBuilder.RenameIndex(
                name: "IX_service_planId",
                table: "userServices",
                newName: "IX_userServices_planId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userServices",
                table: "userServices",
                column: "sId");

            migrationBuilder.AddForeignKey(
                name: "FK_userServices_AspNetUsers_userId",
                table: "userServices",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userServices_Plans_planId",
                table: "userServices",
                column: "planId",
                principalTable: "Plans",
                principalColumn: "planId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userServices_AspNetUsers_userId",
                table: "userServices");

            migrationBuilder.DropForeignKey(
                name: "FK_userServices_Plans_planId",
                table: "userServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userServices",
                table: "userServices");

            migrationBuilder.RenameTable(
                name: "userServices",
                newName: "service");

            migrationBuilder.RenameIndex(
                name: "IX_userServices_userId",
                table: "service",
                newName: "IX_service_userId");

            migrationBuilder.RenameIndex(
                name: "IX_userServices_planId",
                table: "service",
                newName: "IX_service_planId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_service",
                table: "service",
                column: "sId");

            migrationBuilder.AddForeignKey(
                name: "FK_service_AspNetUsers_userId",
                table: "service",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_service_Plans_planId",
                table: "service",
                column: "planId",
                principalTable: "Plans",
                principalColumn: "planId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
