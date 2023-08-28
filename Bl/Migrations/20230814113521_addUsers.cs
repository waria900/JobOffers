using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bl.Migrations
{
    /// <inheritdoc />
    public partial class addUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbApplyForJob_AspNetUsers1",
                table: "TbApplyForJob");

            migrationBuilder.AddForeignKey(
                name: "FK_TbApplyForJob_AspNetUsers",
                table: "TbApplyForJob",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbApplyForJob_AspNetUsers",
                table: "TbApplyForJob");

            migrationBuilder.AddForeignKey(
                name: "FK_TbApplyForJob_AspNetUsers1",
                table: "TbApplyForJob",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
