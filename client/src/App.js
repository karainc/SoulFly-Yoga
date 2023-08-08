import { BrowserRouter as Router, Route, Routes } from "react-router-dom"
import {ApplicationViews} from "./ApplicationViews"
import Authorize from './components/Authorize';
import NavBar from "./components/Navbar/NavBar";
import { useEffect } from 'react';
import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.css';



const App=() => {

  const [isLoggedIn, setIsLoggedIn] = useState(true);


    useEffect(() => {
        if (!localStorage.getItem("users")) {
            setIsLoggedIn(false)

        }
    }, [isLoggedIn])

    return (
      <Router>
          <NavBar isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} />
          {isLoggedIn ?
              <ApplicationViews />
              :
              <Authorize setIsLoggedIn={setIsLoggedIn} />
          }
      </Router>
  );
}
export default App;
