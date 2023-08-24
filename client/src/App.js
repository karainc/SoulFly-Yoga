import { BrowserRouter as Router } from "react-router-dom"
import {ApplicationViews} from "./ApplicationViews"
import Authorize from './components/Authorize';
import { useEffect } from 'react';
import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import Header from "./components/Header";
import './index.css';
// import Goals from "../goals/Goals"
import RingLoader from "react-spinners/RingLoader";
import { UncontrolledCarousel } from "reactstrap";


const App=() => {

  const [isLoggedIn, setIsLoggedIn] = useState(true);


    useEffect(() => {
        if (!localStorage.getItem("users")) {
            setIsLoggedIn(false)

        }
    }, [isLoggedIn])

    return (
        <><>
            <Router>
                <RingLoader color="#36d7b7" type="grow"
                    children={false} />

                <Header isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} />

                {isLoggedIn ?
                    <ApplicationViews />
                    :
                    <Authorize setIsLoggedIn={setIsLoggedIn} />}
            </Router></><div className="carousel-container">
                <><UncontrolledCarousel className="carousel"
                    items={[
                        {
                            caption: 'SoulFly Yoga',
                            caption: 'Quiet Your Mind',
                            key: 1,
                            src: 'https://media.istockphoto.com/id/1074959548/photo/beautiful-attractive-asian-woman-practice-yoga-lotus-pose-on-the-pool-above-the-mountain-peak.jpg?b=1&s=612x612&w=0&k=20&c=lHRVgsKyr3GgTMXYVoIk_gF-iVJRk9lGwp6KwXy3smk='
                        },
                        {
                            altText: 'SoulFly Yoga',
                            caption: 'Relax Your Body',
                            key: 2,
                            src: 'https://c0.wallpaperflare.com/preview/56/956/1001/yoga-zen-meditating-pose.jpg'
                        },
                        {
                            caption: 'SoulFly Yoga',

                            key: 3,
                            src: 'https://wallpaperaccess.com/full/139118.jpg'
                        }
                    ]} /></></div></>
  );
}
export default App;
