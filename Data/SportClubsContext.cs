using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SportClubs1.Data;

public partial class SportClubsContext : DbContext
{
    public SportClubsContext()
    {
    }

    public SportClubsContext(DbContextOptions<SportClubsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Gym> Gyms { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<TrainingMachine> TrainingMachines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= DESKTOP-QGR2O71\\SQLEXPRESS; Database=SportClubs; Trusted_Connection=True; Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Table_1");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Pfp).HasColumnName("pfp");
            entity.Property(e => e.SubscriptionId).HasColumnName("Subscription_ID");
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.Login).HasColumnType("nvarchar(MAX)");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Clients)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clients_Subscriptions");

            entity.HasMany(d => d.Staff).WithMany(p => p.Clients)
                .UsingEntity<Dictionary<string, object>>(
                    "ClientsAndStaff",
                    r => r.HasOne<Staff>().WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ClientsAndStaff_Staff"),
                    l => l.HasOne<Client>().WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ClientsAndStaff_Clients"),
                    j =>
                    {
                        j.HasKey("ClientId", "StaffId");
                        j.ToTable("ClientsAndStaff");
                        j.IndexerProperty<int>("ClientId").HasColumnName("Client_ID");
                        j.IndexerProperty<int>("StaffId").HasColumnName("Staff_ID");
                    });
        });

        modelBuilder.Entity<Gym>(entity =>
        {
            entity.HasKey(e => e.Address);

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Info).HasColumnType("nvarchar(MAX)");


            entity.HasMany(d => d.Clients).WithMany(p => p.GymAddresses)
                .UsingEntity<Dictionary<string, object>>(
                    "ClientsAndGym",
                    r => r.HasOne<Client>().WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ClientsAndGyms_Clients"),
                    l => l.HasOne<Gym>().WithMany()
                        .HasForeignKey("GymAddress")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ClientsAndGyms_Gyms"),
                    j =>
                    {
                        j.HasKey("GymAddress", "ClientId");
                        j.ToTable("ClientsAndGyms");
                        j.IndexerProperty<string>("GymAddress").HasMaxLength(50);
                        j.IndexerProperty<int>("ClientId").HasColumnName("Client_ID");
                    });
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedule");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.HourPerDay).HasColumnName("Hour_per_day");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.StaffId).HasColumnName("Staff_ID");
            entity.Property(e => e.GymAddress)
                .HasMaxLength(50)
                .HasColumnName("Gym_Address");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Pfp).HasColumnName("pfp");
            entity.Property(e => e.Profession).HasMaxLength(50);
            entity.Property(e => e.ScheduleId).HasColumnName("Schedule_ID");
            entity.Property(e => e.Sername).HasMaxLength(50);
            entity.Property(e => e.Info).HasColumnType("nvarchar(MAX)");

            entity.HasOne(d => d.GymAddressNavigation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.GymAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Gyms");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Staff)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Schedule");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<TrainingMachine>(entity =>
        {
            entity.HasKey(e => e.InventNum);

            entity.ToTable("Training Machines");

            entity.Property(e => e.GymAddress)
                .HasMaxLength(50)
                .HasColumnName("Gym_Address");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(20);

            entity.HasOne(d => d.GymAddressNavigation).WithMany(p => p.TrainingMachines)
                .HasForeignKey(d => d.GymAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Training Machines_Gyms");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
