import React from "react";
import { Link } from "react-router-dom";
// reactstrap components
import {
    UncontrolledCollapse,
    NavbarBrand,
    Navbar,
    NavItem,
    NavLink,
    Nav,
    Container
  } from "reactstrap";

function NavBar(){
    return (
      <>
<Navbar className="bg-info" expand="lg">
<Container>
  <div className="navbar-translate">
    <NavbarBrand href="#pablo" onClick={e => e.preventDefault()}>
      Info color
    </NavbarBrand>
    <button
      className="navbar-toggler"
      id="example-navbar-info"
      type="button"
    >
      <span className="navbar-toggler-bar bar1"></span>
      <span className="navbar-toggler-bar bar2"></span>
      <span className="navbar-toggler-bar bar3"></span>
    </button>
  </div>
  <UncontrolledCollapse navbar toggler="#example-navbar-info">
    <Nav className="ml-auto" navbar>
      <NavItem className="active">
        <NavLink href="#pablo" onClick={e => e.preventDefault()}>
          <i className="now-ui-icons objects_globe"></i>
          <p>Discover</p>
        </NavLink>
      </NavItem>
      <NavItem>
        <NavLink href="#pablo" onClick={e => e.preventDefault()}>
          <i className="now-ui-icons users_circle-08"></i>
          <p>Profile</p>
        </NavLink>
      </NavItem>
      <NavItem>
        <NavLink href="#pablo" onClick={e => e.preventDefault()}>
          <i className="now-ui-icons ui-1_settings-gear-63"></i>
          <p>Settings</p>
        </NavLink>
      </NavItem>
    </Nav>
  </UncontrolledCollapse>
</Container>
</Navbar>
</>
)
}
export default NavBar;