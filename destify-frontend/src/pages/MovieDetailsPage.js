import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import axios from "../services/axiosConfig";
import "../styles/MovieDetailsPage.css";

function MovieDetailsPage() {
    const { id } = useParams();
    const [movie, setMovie] = useState(null);
    const [rating, setRating] = useState("");
    const [comment, setComment] = useState("");
    const [successMessage, setSuccessMessage] = useState("");
    const [errorMessage, setErrorMessage] = useState("");

    const fetchMovie = async () => {
        try {
            const response = await axios.get(`/movies/${id}`);
            setMovie(response.data);
        } catch (error) {
            console.error("Error fetching movie details:", error);
        }
    };

    useEffect(() => {
        fetchMovie();
    }, [id]);

    const handleRatingSubmit = async () => {
        if (!rating || rating < 1 || rating > 5) {
            setErrorMessage("Please provide a rating between 1 and 5.");
            return;
        }

        try {
            await axios.post(`/movies/${id}/ratings`, {
                rating: parseInt(rating, 10),
                comment,
            });
            setSuccessMessage("Rating submitted successfully!");
            setErrorMessage("");
            setRating("");
            setComment("");
            fetchMovie(); 
        } catch (error) {
            setErrorMessage("Failed to submit rating. Please try again.");
            console.error("Error submitting rating:", error);
        }
    };

    if (!movie) return <p>Loading...</p>;

    return (
        <div className="movie-details">
            <h2>{movie.title}</h2>
            <p>Release Year: {movie.releaseYear}</p>

            <h3>Ratings</h3>
            <ul className="ratings-list">
                {movie.ratings.map((r, index) => (
                    <li key={index}>
                        <span>{r.rating} ⭐</span>
                        <span>{r.comment || "No comment"}</span>
                    </li>
                ))}
            </ul>

            <h3>Actors</h3>
            <ul className="actors-list">
                {movie.actors.map((actor) => (
                    <li key={actor.id}>{actor.name}</li>
                ))}
            </ul>

            <div className="rating-section">
                <h4>Give Your Rating</h4>
                <input
                    type="number"
                    min="1"
                    max="5"
                    value={rating}
                    onChange={(e) => setRating(e.target.value)}
                    placeholder="Enter rating (1-5)"
                />
                <textarea
                    placeholder="Leave a comment (optional)"
                    value={comment}
                    onChange={(e) => setComment(e.target.value)}
                />
                <button onClick={handleRatingSubmit}>Submit Rating</button>
                {successMessage && <p className="success-message">{successMessage}</p>}
                {errorMessage && <p className="error-message">{errorMessage}</p>}
            </div>
        </div>
    );
}

export default MovieDetailsPage;
