using System;
using System.Collections.Generic;

namespace AI_Friendly_Calendar.Models
{
    /// <summary>
    /// Represents an event in the calendar.
    /// </summary>
    public class Event
    {
        public int Id { get; set; } // Unique identifier for the event
        public string Title { get; set; } = string.Empty; // Event title
        public string? Description { get; set; } // Optional description
        public DateTime StartTime { get; set; } // Start of the event
        public DateTime EndTime { get; set; } // End of the event

        // List of Participants related to this event
        public List<Participant> Participants { get; set; } = new List<Participant>();
    }
}
