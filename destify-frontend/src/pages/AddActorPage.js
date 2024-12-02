import React, { useState } from "react";
import axios from "../services/axiosConfig";
import "../styles/Form.css";
import { useNavigate } from "react-router-dom";

function AddActorPage() {
  const [name, setName] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false); 
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (isSubmitting) return; 

    try {
      setIsSubmitting(true);
      await axios.post("/actors", { name });
      alert("Actor added successfully!");
      navigate("/actors");
    } catch (error) {
      console.error("Error adding actor:", error);
      alert("Error adding actor.");
    } finally {
      setIsSubmitting(false); 
    }
  };

  return (
    <div className="form-container">
      <h2>Add Actor</h2>
      <form onSubmit={handleSubmit}>
        <label htmlFor="name">Name</label>
        <input
          type="text"
          id="name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />

        <button type="submit" disabled={isSubmitting}>
          {isSubmitting ? "Adding..." : "Add Actor"}
        </button>
      </form>
    </div>
  );
}

export default AddActorPage;
