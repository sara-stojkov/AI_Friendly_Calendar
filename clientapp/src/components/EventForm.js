// clientapp/src/components/EventForm.js

import React, { useState } from "react";
import axios from "axios";

function EventForm({ onAdd }) {
  const [title, setTitle] = useState("");
  const [start, setStart] = useState("");
  const [end, setEnd] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    await axios.post("/api/v1/events", {
      title,
      startTime: start,
      endTime: end,
    });
    onAdd();
  };

  return (
    <form onSubmit={handleSubmit}>
      <h3>Add Event</h3>
      <input placeholder="Title" value={title} onChange={(e) => setTitle(e.target.value)} />
      <input type="datetime-local" value={start} onChange={(e) => setStart(e.target.value)} />
      <input type="datetime-local" value={end} onChange={(e) => setEnd(e.target.value)} />
      <button type="submit">Add</button>
    </form>
  );
}

export default EventForm;
