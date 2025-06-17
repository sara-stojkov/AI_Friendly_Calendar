using Microsoft.EntityFrameworkCore;

namespace AI_Friendly_Calendar.Models
{
    /// <summary>
    /// This is your database context.
    /// It tells Entity Framework how to map your C# classes to database tables.
    /// </summary>
    public class CalendarDbContext : DbContext
    {
        public CalendarDbContext(DbContextOptions<CalendarDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}
