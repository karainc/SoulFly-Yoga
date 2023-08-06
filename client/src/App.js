import { BrowserRouter as Router, Route, Routes } from "react-router-dom"
import {ApplicationViews} from "./ApplicationViews"
import Authorize from './components/Authorize';
import './App.css';
import { useEffect } from 'react';
import React, { useState } from 'react';
import Login from "./Pages/Login";


const App=() => {

  const [isLoggedIn, setIsLoggedIn] = useState(true);


    useEffect(() => {
        if (!localStorage.getItem("userProfile")) {
            setIsLoggedIn(false)

        }
    }, [isLoggedIn])

  return (
    
        <Router>
          
          <Login />
            {isLoggedIn ?
                <ApplicationViews />
                :
                <Authorize setIsLoggedIn={setIsLoggedIn} />
            }
        </Router>
    );
}

export default App;
