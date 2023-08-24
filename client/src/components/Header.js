import React, { useState } from 'react';
import { NavLink as RRNavLink } from "react-router-dom";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink
} from 'reactstrap';
import { logout } from "../Managers/UsersManager";
import './Header.css';
import { UncontrolledCarousel } from 'reactstrap';


export default function Header({isLoggedIn, setIsLoggedIn}) {
  const [isOpen, setIsOpen] = useState(false);
  const toggle = () => setIsOpen(!isOpen);

  return (
    <div>
      <Navbar color='primary' expand="lg">
        <NavbarBrand href="#home" bsprefix="navbar-brand-custom" tag={RRNavLink} to="/">SoulFly Yoga</NavbarBrand>
        <NavbarToggler aria-controls="basic-navbar-nav" onClick={toggle} />
        <Collapse id="basic-navbar-nav" isOpen={isOpen} navbar>
          <Nav className="mr-auto" navbar>
            {isLoggedIn &&
               <>
               <NavItem>
                 <NavLink href="#home" tag={RRNavLink} to="/">Home</NavLink>
               </NavItem>
               <NavItem>
                 <NavLink href="#routines" tag={RRNavLink} to="/routines">Routines</NavLink>
               </NavItem>
               <NavItem>
                 <NavLink href="#library" tag={RRNavLink} to="/library">Yoga Library</NavLink>
               </NavItem></>
             }
               {/* {isLoggedIn &&
               <NavItem>
                 <NavLink href="#pablo" tag={RRNavLink} to="/myRoutines">My Routines</NavLink>
               </NavItem>
             } */}
                {/* {isLoggedIn &&
               <NavItem>
                 <NavLink href="#library" tag={RRNavLink} to="/library">Yoga Library</NavLink>
               </NavItem>
             } */}
             
             
             {isLoggedIn &&
               <>
                 <NavItem>
                   <a aria-current="page" className="nav-link"
                     style={{ cursor: "pointer" }} onClick={() => {
                       logout()
                       setIsLoggedIn(false)
                     }}>Logout</a>
                 </NavItem>
               </>
             }
             
             {!isLoggedIn &&
               <><>
                <NavItem>
                  <NavLink href="#home" tag={RRNavLink} to="/login">Login</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink href="#home" tag={RRNavLink} to="/register">Register</NavLink>
                </NavItem>
              </><div className="carousel-container">
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
               
             }
           </Nav>
         </Collapse>
       </Navbar>
     </div>
     
   );
 }