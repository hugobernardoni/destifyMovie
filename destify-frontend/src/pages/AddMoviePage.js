import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import axios from "../services/axiosConfig";
import "../styles/Form.css";

function AddMoviePage() {
  const [title, setTitle] = useState("");
  const [releaseYear, setReleaseYear] = useState("");
  const [actors, setActors] = useState([]);
  const [selectedActors, setSelectedActors] = useState([]);
  const [isLoading, setIsLoading] = useState(true); 
  const navigate = useNavigate();

  useEffect(() => {
    const fetchActors = async () => {
      try {
        const response = await axios.get("/actors");
        setActors(response.data);
      } catch (error) {
        console.error("Error fetching actors:", error);
        alert("Failed to load actors. Please try again.");
      } finally {
        setIsLoading(false); 
      }
    };

    fetchActors();
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const actorIds = selectedActors.map((actor) => actor.id);
      const movieData = {
        title,
        releaseYear: parseInt(releaseYear),
        actorIds,
      };

      await axios.post("/movies", movieData);
      alert("Movie added successfully!");
      navigate("/movies");
    } catch (error) {
      console.error("Error adding movie:", error);
      alert("Failed to add movie. Please try again.");
    }
  };

  const handleActorSelection = (e) => {
    const actorId = parseInt(e.target.value);
    const actor = actors.find((actor) => actor.id === actorId);

    if (!selectedActors.some((a) => a.id === actorId)) {
      setSelectedActors([...selectedActors, actor]);
    }
  };

  const removeSelectedActor = (actorId) => {
    setSelectedActors(selectedActors.filter((actor) => actor.id !== actorId));
  };

  if (isLoading) {
    return <div className="form-container">Loading...</div>;
  }

  return (
    <div className="form-container">
      <h2>Add Movie</h2>
      <form onSubmit={handleSubmit}>
        <label htmlFor="title">Title</label>
        <input
          type="text"
          id="title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          required
        />

        <label htmlFor="releaseYear">Release Year</label>
        <input
          type="number"
          id="releaseYear"
          value={releaseYear}
          onChange={(e) => setReleaseYear(e.target.value)}
          required
        />

        <label htmlFor="actors">Actors</label>
        <select id="actors" onChange={handleActorSelection} defaultValue="">
          <option value="" disabled>
            Select an actor
          </option>
          {actors.map((actor) => (
            <option key={actor.id} value={actor.id}>
              {actor.name}
            </option>
          ))}
        </select>

        <div className="selected-actors">
          <h4>Selected Actors:</h4>
          <ul>
            {selectedActors.map((actor) => (
              <li key={actor.id}>
                {actor.name}{" "}
                <button
                  type="button"
                  onClick={() => removeSelectedActor(actor.id)}
                >
                  Remove
                </button>
              </li>
            ))}
          </ul>
        </div>

        <button type="submit">Add Movie</button>
      </form>
    </div>
  );
}

export default AddMoviePage;
