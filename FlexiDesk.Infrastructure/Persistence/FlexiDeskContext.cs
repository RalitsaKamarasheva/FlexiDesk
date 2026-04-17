using FlexiDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlexiDesk.Infrastructure.Persistence
{
    public class FlexiDeskContext:DbContext
    {
        public FlexiDeskContext(DbContextOptions<FlexiDeskContext> options) : base(options) { }
        // Твоите таблици
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация за Resource
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); // Автоматичен GUID в SQL
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            // Конфигурация за Reservation
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.HasOne(d => d.Resource)
                      .WithMany(p => p.Reservations)
                      .HasForeignKey(d => d.ResourceId);

                // Индексът вече ползва Guid за ResourceId
                entity.HasIndex(e => new { e.ResourceId, e.StartTime, e.EndTime })
                      .HasDatabaseName("IX_Reservation_ConflictCheck");
            });

            // Конфигурация за AuditLog
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); // Автоматичен GUID в SQL
                entity.Property(e => e.EntityName).IsRequired();
                entity.Property(e => e.EntityId).IsRequired();
            });

            // Временно Seed-ване (използваме фиксирани GUID за теста)
            var deskId = Guid.Parse("7f345678-1234-1234-1234-1234567890ab");
            modelBuilder.Entity<Resource>().HasData(
                new Resource { Id = deskId, Name = "Desk Alpha-1", Type = "Desk", PricePerHour = 10.00m }
            );
        }
    }
}
