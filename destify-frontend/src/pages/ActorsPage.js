import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import axios from "../services/axiosConfig";
import "../styles/ActorsPage.css";

function ActorsPage() {
  const [actors, setActors] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");
  const token = localStorage.getItem("token");
  const navigate = useNavigate();

  useEffect(() => {
    fetchActors();
  }, []);

  const fetchActors = async (query = "") => {
    try {
      const endpoint = query
        ? `/actors/search?name=${encodeURIComponent(query)}`
        : "/actors";
      const response = await axios.get(endpoint);
      setActors(response.data);
    } catch (error) {
      console.error("Error fetching actors:", error);
    }
  };

  const handleAddActor = () => {
    navigate("/actor/add");
  };

  const handleSearch = (e) => {
    e.preventDefault();
    fetchActors(searchQuery);
  };

  return (
    <div className="actors-page">
      <h2>Actors</h2>
      <div className="actions">
        {token && (
          <button className="add-button" onClick={handleAddActor}>
            Add Actor
          </button>
        )}
        <form onSubmit={handleSearch} className="search-form">
          <input
            type="text"
            placeholder="Search by name"
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="search-input"
          />
          <button type="submit" className="search-button">
            Search
          </button>
        </form>
      </div>
      <div className="actor-grid">
        {actors.map((actor) => (
          <div key={actor.id} className="actor-card">
            <h3>{actor.name}</h3>
            {actor.movies.length > 0 && (
              <ul className="actor-movies-list">
                {actor.movies.map((movie) => (
                  <li key={movie.id}>{movie.title}</li>
                ))}
              </ul>
            )}
          </div>
        ))}
      </div>
    </div>


  );
}

export default ActorsPage;
