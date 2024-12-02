import React, { useContext } from "react";
import { Routes, Route } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import MoviesPage from "./pages/MoviesPage";
import ActorsPage from "./pages/ActorsPage";
import MovieDetailsPage from "./pages/MovieDetailsPage";
import AddMoviePage from "./pages/AddMoviePage";
import AddActorPage from "./pages/AddActorPage";
import Navbar from "./components/Navbar";
import { AuthContext, AuthProvider } from "./context/AuthContext";

function AppRoutes() {
  const { token } = useContext(AuthContext);

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

function App() {
  return (
    <AuthProvider>
      <AppRoutes />
    </AuthProvider>
  );
}

export default App;
