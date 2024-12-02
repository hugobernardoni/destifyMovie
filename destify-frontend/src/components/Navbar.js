import React, { useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";
import "../styles/Navbar.css";

function Navbar() {
  const { token, logout } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleLogoClick = () => {
    if (token) {
      navigate("/movies");
    } else {
      navigate("/");
    }
  };

  return (
    <nav className="navbar">
      <div className="navbar-container">
        <span className="navbar-logo" onClick={handleLogoClick}>
          Destify Movie
        </span>
        <ul className="navbar-links">
          <li>
            <Link to="/movies">Movies</Link>
          </li>
          <li>
            <Link to="/actors">Actors</Link>
          </li>
          {token && (
            <>
              <li>
                <Link to="/movie/add">Add Movie</Link>
              </li>
              <li>
                <Link to="/actor/add">Add Actor</Link>
              </li>
              <li>
                <button className="logout-button" onClick={logout}>
                  Logout
                </button>
              </li>
            </>
          )}
        </ul>
      </div>
    </nav>
  );
}

export default Navbar;
