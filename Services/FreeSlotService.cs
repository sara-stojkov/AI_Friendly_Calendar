using AI_Friendly_Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_Friendly_Calendar.Services
{
    public class FreeSlotService : IFreeSlotService
    {
        public TimeRange? FindEarliestFreeSlot(DateTime from, DateTime to, TimeSpan duration, List<Event> busyEvents)
        {
            var busy = busyEvents
                .Select(e => new TimeRange { Start = e.StartTime, End = e.EndTime })
                .OrderBy(r => r.Start)
                .ToList();

            var merged = new List<TimeRange>();
            foreach (var slot in busy)
            {
                if (merged.Count == 0 || merged.Last().End < slot.Start)
                {
                    merged.Add(slot);
                }
                else
                {
                    merged.Last().End = slot.End > merged.Last().End ? slot.End : merged.Last().End;
                }
            }

            DateTime checkStart = from;

            foreach (var b in merged)
            {
                if (b.Start - checkStart >= duration)
                    return new TimeRange { Start = checkStart, End = checkStart + duration };

                checkStart = b.End > checkStart ? b.End : checkStart;
            }

            if (to - checkStart >= duration)
                return new TimeRange { Start = checkStart, End = checkStart + duration };

            return null;
        }
    }

    public class TimeRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
