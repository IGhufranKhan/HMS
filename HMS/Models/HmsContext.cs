using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HMS.Models;

public partial class HmsContext : DbContext
{
    public HmsContext()
    {
    }

    public HmsContext(DbContextOptions<HmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Billing> Billings { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HP\\SQLEXPRESS;Database=HMS;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Address__3214EC07E3A022A9");

            entity.ToTable("Address");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.PostalCode).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Street).HasMaxLength(255);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC07838C0B64");

            entity.ToTable("Appointment");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AppointmentDate).HasColumnType("datetime");
            entity.Property(e => e.IsCompleted).HasDefaultValueSql("((0))");
            entity.Property(e => e.Purpose).HasMaxLength(255);

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Appointment_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Appointment_Patient");
        });

        modelBuilder.Entity<Billing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Billing__3214EC07DC0E3685");

            entity.ToTable("Billing");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BillingDate).HasColumnType("datetime");
            entity.Property(e => e.IsPaid).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Billings)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Billing_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Billings)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Billing_Patient");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC07B512A790");

            entity.ToTable("Department");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.HeadOfDepartment).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctor__3214EC07129872E6");

            entity.ToTable("Doctor");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Department).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Doctor_Department");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Members_1");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.JoinDate).HasColumnType("date");
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.MembershipType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient__3214EC07DF87B805");

            entity.ToTable("Patient");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AdmissionDate).HasColumnType("datetime");
            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.DischargeDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Address).WithMany(p => p.Patients)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Patient_Address");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Patients)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Patient_Doctor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
