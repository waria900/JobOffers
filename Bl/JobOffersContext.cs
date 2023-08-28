using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bl;

public partial class JobOffersContext : IdentityDbContext<ApplicationUser>
{
    public JobOffersContext()
    {
    }

    public JobOffersContext(DbContextOptions<JobOffersContext> options)
        : base(options)
    {
    }


   



  //  public virtual DbSet<TbApplyForJob> TbApplyForJobs { get; set; }
  //  public virtual DbSet<VwApplyJob> VwApplyJobs { get; set; }


    public virtual DbSet<TbCategory> TbCategories { get; set; }

    public virtual DbSet<TbJob> TbJobs { get; set; }
    public virtual DbSet<VwJob> VwJobs { get; set; }


    public virtual DbSet<TbJobLocation> TbJobLocations { get; set; }

    public virtual DbSet<TbJobType> TbJobTypes { get; set; }



    public virtual DbSet<TbApplyForAJob> TbApplyForAJobs { get; set; }
    public virtual DbSet<VwApplyForAJob> VwApplyForAJobs { get; set; }






    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);



        //modelBuilder.Entity<TbApplyForJob>(entity =>
        //{
        //    entity.HasKey(e => e.ApplyId);

        //    entity.ToTable("TbApplyForJob");

        //    entity.Property(e => e.Message).HasMaxLength(500);
        //    entity.Property(e => e.UserId).HasMaxLength(450);

        //    entity.HasOne(d => d.Job).WithMany(p => p.TbApplyForJobs)
        //        .HasForeignKey(d => d.JobId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_TbApplyForJob_TbJobs");

        //    entity.HasOne(d => d.User).WithMany(p => p.TbApplyForJobs)
        //        .HasForeignKey(d => d.UserId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_TbApplyForJob_AspNetUsers");
        //});

        modelBuilder.Entity<TbCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("TbCategory");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.ImageName).IsUnicode(false);
        });

        modelBuilder.Entity<TbJob>(entity =>
        {
            entity.HasKey(e => e.JobId);

            entity.Property(e => e.CompanyAddress).HasMaxLength(100);
            entity.Property(e => e.CompanyDetails).HasMaxLength(100);
            entity.Property(e => e.CompanyEmail).HasMaxLength(100);
            entity.Property(e => e.CompanyName).HasMaxLength(100);
            entity.Property(e => e.JobName).HasMaxLength(100);
            entity.Property(e => e.MaxSalary).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.MinSalary).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Category).WithMany(p => p.TbJobs)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbJobs_TbCategory");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.InverseCategoryNavigation)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbJobs_TbJobs");

            entity.HasOne(d => d.JobLocation).WithMany(p => p.TbJobs)
                .HasForeignKey(d => d.JobLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbJobs_TbJobLocation");

            entity.HasOne(d => d.JobType).WithMany(p => p.TbJobs)
                .HasForeignKey(d => d.JobTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbJobs_TbJobType");
        });

        modelBuilder.Entity<TbJobLocation>(entity =>
        {
            entity.HasKey(e => e.JobLocationId);

            entity.ToTable("TbJobLocation");

            entity.Property(e => e.JobLocationName).HasMaxLength(50);
        });

        modelBuilder.Entity<TbJobType>(entity =>
        {
            entity.HasKey(e => e.JobTypeId);

            entity.ToTable("TbJobType");

            entity.Property(e => e.JobTypeName).HasMaxLength(50);
        });


        modelBuilder.Entity<VwJob>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("VwJobs");
        });


        //modelBuilder.Entity<VwApplyJob>(entity =>
        //{
        //    entity.HasNoKey();
        //    entity.ToView("VwApplyJobs");
        //});


        modelBuilder.Entity<TbApplyForAJob>(entity =>
        {
            entity.HasKey(e => e.ApplyId);

            entity.HasOne(e => e.Job).
            WithMany(e => e.TbApplyForAJobs).
            HasForeignKey(e => e.JobId);


            entity.HasOne(e => e.User).
            WithMany(e => e.TbApplyForAJobs).
            HasForeignKey(e => e.UserId);

        });

        modelBuilder.Entity<VwApplyForAJob>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("VwApplyForAJobs");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
