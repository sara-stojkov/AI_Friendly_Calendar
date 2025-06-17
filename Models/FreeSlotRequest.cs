namespace AI_Friendly_Calendar.Models
{
    public class FreeSlotRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public TimeSpan Duration { get; set; }
        public List<int> UserIds { get; set; }
    }
}
