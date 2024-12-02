import React from "react";
import { Routes, Route } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import MoviesPage from "./pages/MoviesPage";
import ActorsPage from "./pages/ActorsPage";
import MovieDetailsPage from "./pages/MovieDetailsPage";
import AddMoviePage from "./pages/AddMoviePage";
import AddActorPage from "./pages/AddActorPage";
import Navbar from "./components/Navbar";

function App() {
  const token = localStorage.getItem("token");

  return (
    <>
      <Navbar />
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/movies" element={<MoviesPage />} />
        <Route path="/movies/:id" element={<MovieDetailsPage />} />
        <Route path="/actors" element={<ActorsPage />} />
        {token && (
          <>
            <Route path="/movie/add" element={<AddMoviePage />} />
            <Route path="/actor/add" element={<AddActorPage />} />
          </>
        )}
      </Routes>
    </>
  );
}

export default App;
