using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bl.Migrations
{
    /// <inheritdoc />
    public partial class VwApplyForAJobView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Create View VwApplyForAJobs
                                    as
                                    SELECT dbo.TbApplyForAJobs.ApplyId, dbo.TbApplyForAJobs.Message, dbo.TbApplyForAJobs.ApplyDate, dbo.TbApplyForAJobs.PdfResume, dbo.TbApplyForAJobs.CurrentState, dbo.TbApplyForAJobs.JobId, dbo.TbApplyForAJobs.UserId, 
                                    dbo.TbJobs.JobName, dbo.TbJobs.CompanyName, dbo.TbJobs.CompanyLogo, dbo.AspNetUsers.firstName, dbo.AspNetUsers.secondName
                                    FROM     dbo.TbApplyForAJobs INNER JOIN
                                    dbo.AspNetUsers ON dbo.TbApplyForAJobs.UserId = dbo.AspNetUsers.Id INNER JOIN
                                    dbo.TbJobs ON dbo.TbApplyForAJobs.JobId = dbo.TbJobs.JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
