// clientapp/src/App.js

import React, { useState } from "react";
import LoginForm from "./components/LoginForm";
import Calendar from "./components/Calendar"; // ✅ Your Calendar component
// clientapp/src/App.js
import 'react-calendar/dist/Calendar.css';

function App() {
  const [user, setUser] = useState(null);

  return (
    <div>
      {user ? (
        <Calendar user={user} />
      ) : (
        <LoginForm onLogin={setUser} />
      )}
    </div>
  );
}

export default App;
