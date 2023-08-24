import React from "react";
import { Link } from "react-router-dom";
// reactstrap components
import {
    NavbarBrand,
    Navbar,
    NavItem,
    NavLink,
    Nav,
    Container,
    Button,
    Collapse
  } from "reactstrap";

function NavBar(){
    return (
      <>
<Navbar className="bg-info" expand="lg">
<Container>
  <div className="navbar-translate">
    <NavbarBrand href="#pablo" onClick={e => e.preventDefault()}>
     SoulFly Yoga
    </NavbarBrand>
    <button
      className="navbar-toggler"
      id="example-navbar-info"
      type="button"
    >

    </button>
  </div>
  <Collapse navbar toggler="#example-navbar-info">
    <Nav className="ml-auto" navbar>
      <NavItem className="active">
        <NavLink href="#pablo" onClick={e => e.preventDefault()}>
          <i className="now-ui-icons objects_globe"></i>
          <p>Home</p>
        </NavLink>
      </NavItem>
      <NavItem>
        <NavLink href="#pablo" onClick={e => e.preventDefault()}>
          <i className="now-ui-icons users_circle-08"></i>
          <p>Routines</p>
        </NavLink>
      </NavItem>
      <NavItem>
        <NavLink href="#pablo" onClick={e => e.preventDefault()}>
          <i className="now-ui-icons ui-1_settings-gear-63"></i>
          <p>Yoga Library</p>
        </NavLink>
      </NavItem>
    </Nav>
  </Collapse>
</Container>
</Navbar>
</>
)
}
;