import { BrowserRouter as Router } from "react-router-dom"
import {ApplicationViews} from "./ApplicationViews"
import Authorize from './components/Authorize';
import { useEffect } from 'react';
import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import Header from "./components/Header";
import './index.css';



const App=() => {

  const [isLoggedIn, setIsLoggedIn] = useState(true);


    useEffect(() => {
        if (!localStorage.getItem("users")) {
            setIsLoggedIn(false)

        }
    }, [isLoggedIn])

    return (
      <Router>
          <Header isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} />
          {isLoggedIn ?
              <ApplicationViews />
              :
              <Authorize setIsLoggedIn={setIsLoggedIn} />
          }
      </Router>
  );
}
export default App;
