namespace AI_Friendly_Calendar.Models
{
    /// <summary>
    /// Connects a User to an Event as a participant.
    /// </summary>
    public class Participant
    {
        public int Id { get; set; } // Unique ID for participant entry

        // Foreign key: link to Event
        public int EventId { get; set; }
        public Event? Event { get; set; }

        // Foreign key: link to User
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
