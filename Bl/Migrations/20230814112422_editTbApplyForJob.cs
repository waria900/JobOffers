using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bl.Migrations
{
    /// <inheritdoc />
    public partial class editTbApplyForJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbApplyForJob_AspNetUsers1",
                table: "TbApplyForJob");

            migrationBuilder.DropForeignKey(
                name: "FK_TbApplyForJob_TbJobs",
                table: "TbApplyForJob");

            migrationBuilder.AddForeignKey(
                name: "FK_TbApplyForJob_AspNetUsers_UserId",
                table: "TbApplyForJob",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbApplyForJob_TbJobs_JobId",
                table: "TbApplyForJob",
                column: "JobId",
                principalTable: "TbJobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbApplyForJob_AspNetUsers_UserId",
                table: "TbApplyForJob");

            migrationBuilder.DropForeignKey(
                name: "FK_TbApplyForJob_TbJobs_JobId",
                table: "TbApplyForJob");

            migrationBuilder.AddForeignKey(
                name: "FK_TbApplyForJob_AspNetUsers1",
                table: "TbApplyForJob",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TbApplyForJob_TbJobs",
                table: "TbApplyForJob",
                column: "JobId",
                principalTable: "TbJobs",
                principalColumn: "JobId");
        }
    }
}
