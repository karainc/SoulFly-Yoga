import './Login.css';
import React, { useState } from "react";
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { useNavigate, Link } from "react-router-dom";
import { login } from "../../Managers/UsersManager";


export default function Login({setIsLoggedIn}) {
  const navigate = useNavigate();

  const [email, setEmail] = useState();
  const [password, setPassword] = useState();

  const loginSubmit = (e) => {
    e.preventDefault();
    login({email, password})
      .then(r =>{
      if(r){
      setIsLoggedIn(true)
      navigate('/')
      }
      else{
        alert("Invalid email")
      }
    })
  };
  return (
    <Form className="login-form" onSubmit={loginSubmit}>
      <fieldset>
        Welcome Back to SoulFly Yoga!
        <p></p>
        Please Sign In...
        <p></p>
        <FormGroup>
          <Label for="email">Email</Label>
          <Input id="email" type="text" onChange={e => setEmail(e.target.value)} />
        </FormGroup>
        <FormGroup>
          <Label for="password">Password</Label>
          <Input id="password" type="password" onChange={e => setPassword(e.target.value)} />
        </FormGroup>
        <FormGroup>
          <Button>Login</Button>
        </FormGroup>
        <em>
          Not registered? <Link to="/register">Register</Link>
        </em>
      </fieldset>
    </Form>
  );
}
{/*       
  <Form className="login-form">
     <h2 className="text-center">
          Welcome to SoulFly Yoga</h2>

   <FormGroup>
    <Label>Email</Label>
     <Input type="email" placeholder="Email"/>
  </FormGroup>
    <FormGroup>
     <Label>Password</Label>
      <Input type="password" placeholder="Password"/>
        </FormGroup>
      <Button className="btn-lg btn-dark btn-block">Log In</Button>
       <div className="text-center">
        <a href="/sign-up">Log In</a>
          <span className="p-2">|</span>
        <a href="/forgot-password">Forgot Password?</a>
      </div>
  </Form> */}
      