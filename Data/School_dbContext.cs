using System;
using System.Collections.Generic;
using Gymnasieskola.Models;
using Microsoft.EntityFrameworkCore;

namespace Gymnasieskola.Data;

public partial class School_dbContext : DbContext
{
    public School_dbContext()
    {
    }

    public School_dbContext(DbContextOptions<School_dbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicRecord> AcademicRecords { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=KAPTOP;Database=school_db;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__Academic__BFCFB4DD7B693978");

            entity.ToTable("Academic_records");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.Grade)
                .HasMaxLength(5)
                .HasColumnName("grade");
            entity.Property(e => e.GradingDate).HasColumnName("grading_date");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

            entity.HasOne(d => d.Student).WithMany(p => p.AcademicRecords)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_record_student");

            entity.HasOne(d => d.Subject).WithMany(p => p.AcademicRecords)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_record_subject");

            entity.HasOne(d => d.Teacher).WithMany(p => p.AcademicRecords)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_record_teacher");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__FDF479860287A877");

            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.ClassName)
                .HasMaxLength(20)
                .HasColumnName("class_name");
            entity.Property(e => e.SchoolYear)
                .HasMaxLength(20)
                .HasColumnName("school_year");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Classes)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_teacher_class");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__1963DD9CA66B3EF6");

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.HomeAddress)
                .HasMaxLength(150)
                .HasColumnName("home_address");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNr)
                .HasMaxLength(30)
                .HasColumnName("phone_nr");
            entity.Property(e => e.Profession)
                .HasMaxLength(50)
                .HasColumnName("profession");
            entity.Property(e => e.SocialSecurityNr)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("social_security_nr");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__2A33069A8EC9CAB3");

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.HomeAddress)
                .HasMaxLength(150)
                .HasColumnName("home_address");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNr)
                .HasMaxLength(30)
                .HasColumnName("phone_nr");
            entity.Property(e => e.SocialSecurityNr)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("social_security_nr");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("fk_class_student");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__5004F6600C951A8D");

            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .HasColumnName("subject_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
