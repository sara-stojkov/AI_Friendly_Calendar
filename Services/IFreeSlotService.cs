using AI_Friendly_Calendar.Models;
using System;
using System.Collections.Generic;

namespace AI_Friendly_Calendar.Services
{
    public interface IFreeSlotService
    {
        TimeRange? FindEarliestFreeSlot(DateTime from, DateTime to, TimeSpan duration, List<Event> busyEvents);
    }
}
