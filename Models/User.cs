namespace AI_Friendly_Calendar.Models
{
    /// <summary>
    /// Represents a user of the calendar system.
    /// </summary>
    public class User
    {
        public int Id { get; set; } // Unique user ID
        public string Name { get; set; } = string.Empty; // User's name or username
        public string Email { get; set; } = string.Empty; // User's email

        // In a real app, the passowrd would be stored as a hash
        public string Password { get; set; } = string.Empty; // User's password (plain text here for simplicity)
    }
}
