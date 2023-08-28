using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bl.Migrations
{
    /// <inheritdoc />
    public partial class VwJobTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Create View VwJobs
                                    as
                                    SELECT dbo.TbJobs.JobId, dbo.TbJobs.CompanyName, dbo.TbJobs.JobName, dbo.TbJobs.CompanyDetails, dbo.TbJobs.CompanyAddress, dbo.TbJobs.CompanyLogo, dbo.TbJobs.CompanyWebsite, dbo.TbJobs.CompanyEmail, 
                                    dbo.TbJobs.MinSalary, dbo.TbJobs.MaxSalary, dbo.TbJobs.JobDescription, dbo.TbJobs.RequiredKnowledge, dbo.TbJobs.Education, dbo.TbJobs.PostedDate, dbo.TbJobs.Vacancy, dbo.TbJobs.ApplicationDate, dbo.TbJobs.CurrentState, 
                                    dbo.TbJobs.userId, dbo.TbJobs.CategoryId, dbo.TbJobs.JobTypeId, dbo.TbJobs.JobLocationId, dbo.TbCategory.CategoryName, dbo.TbJobType.JobTypeName, dbo.TbJobLocation.JobLocationName
                                    FROM     dbo.TbJobs INNER JOIN
                                    dbo.TbCategory ON dbo.TbJobs.CategoryId = dbo.TbCategory.CategoryId INNER JOIN
                                    dbo.TbJobLocation ON dbo.TbJobs.JobLocationId = dbo.TbJobLocation.JobLocationId INNER JOIN
                                    dbo.TbJobType ON dbo.TbJobs.JobTypeId = dbo.TbJobType.JobTypeId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
