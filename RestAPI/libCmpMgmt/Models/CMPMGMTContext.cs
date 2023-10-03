using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace libCmpMgmt.Models
{
    public partial class CMPMGMTContext : DbContext
    {
        public CMPMGMTContext()
        {
        }

        public CMPMGMTContext(DbContextOptions<CMPMGMTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CompUser> CompUsers { get; set; }
        public virtual DbSet<Computer> Computers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CompUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__CompUser__1788CC4C2C43DF35");

                entity.ToTable("CompUser");

                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Dept).HasColumnName("dept");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fname");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("lname");

                entity.Property(e => e.Office).HasColumnName("office");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("position");
            });

            modelBuilder.Entity<Computer>(entity =>
            {
                entity.HasKey(e => e.Sn)
                    .HasName("PK__Computer__3214186C6C64095E");

                entity.ToTable("Computer");

                entity.Property(e => e.Sn)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("sn");

                entity.Property(e => e.Formfactor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("formfactor");

                entity.Property(e => e.Hdd).HasColumnName("hdd");

                entity.Property(e => e.Make)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("make");

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("model");

                entity.Property(e => e.Ram).HasColumnName("ram");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Computers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Login_Student");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
