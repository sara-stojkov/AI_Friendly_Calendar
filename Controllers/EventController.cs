using Microsoft.AspNetCore.Mvc;
using AI_Friendly_Calendar.Models;
using System.Linq;

namespace AI_Friendly_Calendar.Controllers
{
    // Resources: Read-only endpoints
    [ApiController]
    [Route("api/v1/events")]
    [ApiExplorerSettings(GroupName = "resources")]
    public class EventResourcesController : ControllerBase
    {
        private readonly CalendarDbContext _context;

        public EventResourcesController(CalendarDbContext context)
        {
            _context = context;
        }

        // GET /api/v1/events
        [HttpGet]
        public IActionResult GetEvents()
        {
            var events = _context.Events.ToList();
            return Ok(events);
        }

        // GET /api/v1/events/{eventId}/participants
        [HttpGet("{eventId}/participants")]
        public IActionResult GetParticipants(int eventId)
        {
            var eventExists = _context.Events.Any(e => e.Id == eventId);
            if (!eventExists)
                return NotFound("Event not found.");

            var participants = _context.Participants
                .Where(p => p.EventId == eventId)
                .ToList();

            return Ok(participants);
        }
    }

    // Tools: Writable endpoints (create/update/delete)
    [ApiController]
    [Route("api/v1/events")]
    [ApiExplorerSettings(GroupName = "tools")]
    public class EventToolsController : ControllerBase
    {
        private readonly CalendarDbContext _context;

        public EventToolsController(CalendarDbContext context)
        {
            _context = context;
        }

        // POST /api/v1/events
        [HttpPost]
        public IActionResult CreateEvent([FromBody] Event newEvent)
        {
            _context.Events.Add(newEvent);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.Id }, newEvent);
        }

        // Helper method for CreatedAtAction
        [HttpGet("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)] // Hide from Swagger, this is only helper
        public IActionResult GetEventById(int id)
        {
            var ev = _context.Events.FirstOrDefault(e => e.Id == id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        // PUT /api/v1/events/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] Event updatedEvent)
        {
            var existing = _context.Events.FirstOrDefault(e => e.Id == id);
            if (existing == null) return NotFound();

            // Update fields as needed
            existing.Title = updatedEvent.Title;
            existing.Date = updatedEvent.Date;
            existing.Description = updatedEvent.Description;
            // add other fields here

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE /api/v1/events/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var existing = _context.Events.FirstOrDefault(e => e.Id == id);
            if (existing == null) return NotFound();

            _context.Events.Remove(existing);
            _context.SaveChanges();
            return NoContent();
        }

        // POST /api/v1/events/{eventId}/participants
        [HttpPost("{eventId}/participants")]
        public IActionResult AddParticipant(int eventId, [FromBody] Participant participant)
        {
            var ev = _context.Events.FirstOrDefault(e => e.Id == eventId);
            if (ev == null) return NotFound("Event not found.");

            participant.EventId = eventId;
            _context.Participants.Add(participant);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetParticipant), new { eventId = eventId, userId = participant.UserId }, participant);
        }

        // GET participant helper
        [HttpGet("{eventId}/participants/{userId}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult RemoveParticipant(int eventId, int userId)
        {
            var participant = _context.Participants.FirstOrDefault(p => p.EventId == eventId && p.UserId == userId);
            if (participant == null) return NotFound();

            _context.Participants.Remove(participant);
            _context.SaveChanges();
            return NoContent();
        }
    }
}