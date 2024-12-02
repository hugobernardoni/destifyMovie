import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import axios from "../services/axiosConfig";
import "../styles/MoviesPage.css";

function MoviesPage() {
  const [movies, setMovies] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");
  const token = localStorage.getItem("token");
  const navigate = useNavigate();

  useEffect(() => {
    fetchMovies();
  }, []);

  const fetchMovies = async (query = "") => {
    try {
      const endpoint = query
        ? `/movies/search?title=${encodeURIComponent(query)}`
        : "/movies";
      const response = await axios.get(endpoint);
      setMovies(response.data);
    } catch (error) {
      console.error("Error fetching movies:", error);
    }
  };

  const handleAddMovie = () => {
    navigate("/movies/add");
  };

  const handleSearch = (e) => {
    e.preventDefault();
    fetchMovies(searchQuery);
  };

  return (
    <div className="movies-page">
      <h2>Movies</h2>
      <div className="actions">
        {token && (
          <button className="add-button" onClick={handleAddMovie}>
            Add Movie
          </button>
        )}
        <form onSubmit={handleSearch} className="search-form">
          <input
            type="text"
            placeholder="Search by title"
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="search-input"
          />
          <button type="submit" className="search-button">
            Search
          </button>
        </form>
      </div>
      <div className="movie-grid">
        {movies.map((movie) => (
          <div key={movie.id} className="movie-card">
            <Link to={`/movies/${movie.id}`} className="movie-link">
              <h3>{movie.title}</h3>
              <p>Release Year: {movie.releaseYear}</p>
              {movie.ratings.length > 0 ? (
                <p>Average Rating: {calculateAverageRating(movie.ratings)}</p>
              ) : (
                <p>No Ratings Yet</p>
              )}
            </Link>
          </div>
        ))}
      </div>
    </div>
  );
}

function calculateAverageRating(ratings) {
  const total = ratings.reduce((sum, rating) => sum + rating.rating, 0);
  return (total / ratings.length).toFixed(1);
}

export default MoviesPage;
