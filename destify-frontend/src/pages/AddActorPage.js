import React, { useState } from "react";
import axios from "../services/axiosConfig";
import "../styles/Form.css";
import { useNavigate } from "react-router-dom";


function AddActorPage() {
  const [name, setName] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post("/actors", { name });
      alert("Actor added successfully!");
      navigate("/actors");
    } catch (error) {
      alert("Error adding actor.");
    }
  };

  return (
    <div className="form-container">
      <h2>Add Actor</h2>
      <form onSubmit={handleSubmit}>
        <label>Name</label>
        <input
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />

        <button type="submit">Add Actor</button>
      </form>
    </div>
  );
}

export default AddActorPage;
