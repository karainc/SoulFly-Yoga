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
               <>
                <NavItem>
                  <NavLink href="#home" tag={RRNavLink} to="/login">Login</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink href="#home" tag={RRNavLink} to="/register">Register</NavLink>
                </NavItem>
              </>
               
             }
           </Nav>
         </Collapse>
       </Navbar>
     </div>
     
   );
 }