import React, { useEffect, useState } from 'react';

function App() {
  const [events, setEvents] = useState([]);
  const [loading, setLoading] = useState(true);

  // Fetch events when the component mounts
  useEffect(() => {
    fetch('/api/v1/events')
      .then(response => {
        if (!response.ok) throw new Error('Failed to fetch events.');
        return response.json();
      })
      .then(data => {
        setEvents(data);
        setLoading(false);
      })
      .catch(error => {
        console.error('Error fetching events:', error);
        setLoading(false);
      });
  }, []);

  return (
    <div style={{ padding: '2rem' }}>
      <h1>📅 AI Friendly Calendar</h1>

      {loading && <p>Loading events...</p>}

      {!loading && events.length === 0 && (
        <p>No events found. Try adding some through the API!</p>
      )}

      {!loading && events.length > 0 && (
        <ul>
          {events.map(event => (
            <li key={event.id}>
              <strong>{event.title}</strong> <br />
              {new Date(event.startTime).toLocaleString()} —{' '}
              {new Date(event.endTime).toLocaleString()}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default App;
