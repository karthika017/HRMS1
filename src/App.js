import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import RegistrationForm from "./component/RegistrationForm";
import Home from "./component/Home";

function App() {
  return (
    <BrowserRouter>
      <Routes>

        {/* Default page → Registration */}
        <Route path="/" element={<RegistrationForm />} />

        {/* Registration page (optional extra route) */}
        <Route path="/register" element={<RegistrationForm />} />

        {/* Home page (employee table + edit) */}
        <Route path="/home" element={<Home />} />

      </Routes>
    </BrowserRouter>
  );
}

export default App;