using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entities;

namespace Ticket.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<Airport> Airports { get; set; }

        public DbSet<Domain.Entities.Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set the primary key
            modelBuilder.Entity<Domain.Entities.Ticket>().HasKey(t => t.Id);

            // Configure properties
            modelBuilder.Entity<Domain.Entities.Ticket>().Property(t => t.Price).IsRequired();
            modelBuilder.Entity<Domain.Entities.Ticket>().Property(t => t.TicketNumber).IsRequired();
            modelBuilder.Entity<Domain.Entities.Ticket>().Property(t => t.DepartureDateTime).IsRequired();
            modelBuilder.Entity<Domain.Entities.Ticket>().Property(t => t.ArrivalDateTime).IsRequired();

            // Configure foreign keys with one using ON DELETE NO ACTION
            modelBuilder.Entity<Domain.Entities.Ticket>()
                .HasOne(t => t.FromAirport)
                .WithMany()
                .HasForeignKey(t => t.FromAirportId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure the other with ON DELETE CASCADE if cascading delete is desired
            modelBuilder.Entity<Domain.Entities.Ticket>()
                .HasOne(t => t.ToAirport)
                .WithMany()
                .HasForeignKey(t => t.ToAirportId)
                .OnDelete(DeleteBehavior.NoAction);

            // Additional configurations as needed

            // Call the base class method
            base.OnModelCreating(modelBuilder);
        }

    }
}
