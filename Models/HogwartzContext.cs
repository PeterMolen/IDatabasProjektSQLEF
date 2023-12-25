using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IDatabasProjektSQLEF.Models;

public partial class HogwartzContext : DbContext
{
    public HogwartzContext()
    {
    }

    public HogwartzContext(DbContextOptions<HogwartzContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<House> Houses { get; set; }

    public virtual DbSet<HouseMember> HouseMembers { get; set; }

    public virtual DbSet<Occupation> Occupations { get; set; }

    public virtual DbSet<Proffesion> Proffesions { get; set; }

    public virtual DbSet<Proffesor> Proffesors { get; set; }

    public virtual DbSet<SetGrade> SetGrades { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teaching> Teachings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog=HogwartzGymnasium;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927A0B2587011");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.ClassInfo).HasMaxLength(70);
            entity.Property(e => e.ClassName).HasMaxLength(30);
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");
            entity.Property(e => e.FkClassId).HasColumnName("FK_ClassID");
            entity.Property(e => e.FkProffesorId).HasColumnName("FK_ProffesorID");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");

            entity.HasOne(d => d.FkClass).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.FkClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__FK_Cl__31EC6D26");

            entity.HasOne(d => d.FkProffesor).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.FkProffesorId)
                .HasConstraintName("FK_Enrollments_Proffesors");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.FkStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__FK_St__30F848ED");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradesId).HasName("PK__Grades__931A40BFB6795E24");

            entity.Property(e => e.GradesId).HasColumnName("GradesID");
            entity.Property(e => e.GradeDateSet).HasColumnType("datetime");
            entity.Property(e => e.GradeSet).HasMaxLength(5);
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.HousesId);

            entity.Property(e => e.HousesId).HasColumnName("HousesID");
            entity.Property(e => e.HouseAnimal).HasMaxLength(10);
            entity.Property(e => e.HouseAttributes)
                .HasMaxLength(75)
                .HasColumnName("House Attributes");
            entity.Property(e => e.HouseCommonRoom).HasMaxLength(50);
            entity.Property(e => e.HouseFounder).HasMaxLength(50);
            entity.Property(e => e.HouseGhost).HasMaxLength(30);
            entity.Property(e => e.HouseHead).HasMaxLength(50);
            entity.Property(e => e.HouseName).HasMaxLength(15);
        });

        modelBuilder.Entity<HouseMember>(entity =>
        {
            entity.HasKey(e => e.Hmid);

            entity.ToTable("HouseMember");

            entity.Property(e => e.Hmid).HasColumnName("HMID");
            entity.Property(e => e.FkHouseId).HasColumnName("FK_HouseID");
            entity.Property(e => e.FkProffesorId).HasColumnName("FK_ProffesorID");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");
        });

        modelBuilder.Entity<Occupation>(entity =>
        {
            entity.HasKey(e => e.Occid);

            entity.ToTable("Occupation");

            entity.Property(e => e.Occid).HasColumnName("OCCID");
            entity.Property(e => e.FkProffesionId).HasColumnName("FK_ProffesionID");
            entity.Property(e => e.FkProffesorId).HasColumnName("FK_ProffesorID");

            entity.HasOne(d => d.FkProffesion).WithMany(p => p.Occupations)
                .HasForeignKey(d => d.FkProffesionId)
                .HasConstraintName("FK__Occupatio__FK_Pr__276EDEB3");

            entity.HasOne(d => d.FkProffesor).WithMany(p => p.Occupations)
                .HasForeignKey(d => d.FkProffesorId)
                .HasConstraintName("FK__Occupatio__FK_Pr__286302EC");
        });

        modelBuilder.Entity<Proffesion>(entity =>
        {
            entity.HasKey(e => e.ProffesionId).HasName("PK__Proffesi__655B8CCC3D544ECB");

            entity.ToTable("Proffesion");

            entity.Property(e => e.ProffesionId).HasColumnName("ProffesionID");
            entity.Property(e => e.MonthlySalary).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Title).HasMaxLength(30);
        });

        modelBuilder.Entity<Proffesor>(entity =>
        {
            entity.HasKey(e => e.ProffesorId).HasName("PK__Proffeso__ED16CB8C7073E4EF");

            entity.Property(e => e.ProffesorId).HasColumnName("ProffesorID");
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.ProffStartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<SetGrade>(entity =>
        {
            entity.Property(e => e.SetGradeId).HasColumnName("SetGradeID");
            entity.Property(e => e.FkClassId).HasColumnName("FK_ClassID");
            entity.Property(e => e.FkGradesId).HasColumnName("FK_GradesID");
            entity.Property(e => e.FkProffesorId).HasColumnName("FK_ProffesorID");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");

            entity.HasOne(d => d.FkClass).WithMany(p => p.SetGrades)
                .HasForeignKey(d => d.FkClassId)
                .HasConstraintName("FK_SetGrades_Classes");

            entity.HasOne(d => d.FkGrades).WithMany(p => p.SetGrades)
                .HasForeignKey(d => d.FkGradesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SetGrades__FK_Gr__36B12243");

            entity.HasOne(d => d.FkProffesor).WithMany(p => p.SetGrades)
                .HasForeignKey(d => d.FkProffesorId)
                .HasConstraintName("FK__SetGrades__FK_Pr__35BCFE0A");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.SetGrades)
                .HasForeignKey(d => d.FkStudentId)
                .HasConstraintName("FK_SetGrades_Students");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A799F4DD445");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Personal)
                .HasMaxLength(13)
                .HasColumnName("Personal#");
            entity.Property(e => e.StudentFirstName).HasMaxLength(40);
            entity.Property(e => e.StudentLastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Teaching>(entity =>
        {
            entity.ToTable("Teaching");

            entity.Property(e => e.TeachingId).HasColumnName("TeachingID");
            entity.Property(e => e.FkClassId).HasColumnName("FK_ClassID");
            entity.Property(e => e.FkProffesorId).HasColumnName("FK_ProffesorID");

            entity.HasOne(d => d.FkClass).WithMany(p => p.Teachings)
                .HasForeignKey(d => d.FkClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Teaching__FK_Cla__2C3393D0");

            entity.HasOne(d => d.FkProffesor).WithMany(p => p.Teachings)
                .HasForeignKey(d => d.FkProffesorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Teaching__FK_Pro__2D27B809");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
