using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bl.Migrations
{
    /// <inheritdoc />
    public partial class addVwApplyJobsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view VwApplyJobs
as
SELECT dbo.TbApplyForJob.Message, dbo.TbApplyForJob.ApplyDate, dbo.TbApplyForJob.PdfResume, dbo.TbApplyForJob.CurrentState, dbo.TbApplyForJob.JobId, dbo.TbApplyForJob.UserId, dbo.AspNetUsers.firstName, 
                  dbo.AspNetUsers.secondName, dbo.TbJobs.JobName, dbo.TbJobs.CompanyName, dbo.TbJobs.CompanyLogo
FROM     dbo.AspNetUsers INNER JOIN
                  dbo.TbApplyForJob ON dbo.AspNetUsers.Id = dbo.TbApplyForJob.UserId INNER JOIN
                  dbo.TbJobs ON dbo.TbApplyForJob.JobId = dbo.TbJobs.JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
