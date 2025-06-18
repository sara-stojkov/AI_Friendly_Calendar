// clientapp/src/components/Calendar.js

import React, { useEffect, useState } from "react";
import axios from "axios";
import EventForm from "./EventForm"; // optional, if you want to add events

function Calendar({ user }) {
  const [events, setEvents] = useState([]);

  // fetch events from your API
  const fetchEvents = async () => {
    const res = await axios.get("/api/v1/events");
    setEvents(res.data);
  };

  useEffect(() => {
    fetchEvents();
  }, []);

  return (
    <div style={{ height: "100vh", background: "#f0f0f0", padding: "20px" }}>
      <h2>Welcome {user.email}!</h2>
      <h3>Your Calendar:</h3>

      {events.length === 0 ? (
        <p>No events yet</p>
      ) : (
        <ul>
          {events.map((e) => (
            <li key={e.id}>
              <strong>{e.title}</strong> — {e.startTime} to {e.endTime}
            </li>
          ))}
        </ul>
      )}

      {/* Optional: form to add events */}
      <EventForm onAdd={fetchEvents} />
    </div>
  );
}

export default Calendar;
