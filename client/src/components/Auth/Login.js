import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import React from 'react';
import './Login.css';
import {
  Card,
  CardHeader,
  CardBody,
  CardFooter,
  InputGroupText,
  InputGroup,
  Container,
  Col
} from "reactstrap";



const Login = () =>{
  const [firstFocus, setFirstFocus] = React.useState(false);
  const [lastFocus, setLastFocus] = React.useState(false);
  React.useEffect(() => {
    document.body.classList.add("login-page");
    document.body.classList.add("sidebar-collapse");
    document.documentElement.classList.remove("nav-open");
    window.scrollTo(0, 0);
    document.body.scrollTop = 0;
    return function cleanup() {
      document.body.classList.remove("login-page");
      document.body.classList.remove("sidebar-collapse");
    };
  }, []);
  return (
  <>
  
    {/* <div className="page-header clear-filter" filter-color="blue"> */}

    <div className="content">
          <Container>
            <Col className="ml-auto mr-auto" md="4">
              <Card className="card-login card-plain">
                <Form action="" className="form" method="">
                  <CardHeader className="text-center">
              
                  </CardHeader>
                  <CardBody>
                    <InputGroup
                      className={
                        "no-border input-lg" +
                        (firstFocus ? " input-group-focus" : "")
                      }
                    >     <h2 className="text-center">
                    Welcome to SoulFly Yoga</h2>
            
                        <InputGroupText>
                          <i className="now-ui-icons users_circle-08"></i>
                        </InputGroupText>
   
                      <Input
                        placeholder="Email..."
                        type="text"
                        onFocus={() => setFirstFocus(true)}
                        onBlur={() => setFirstFocus(false)}
                      ></Input>
                    </InputGroup>
                    <InputGroup
                      className={
                        "no-border input-lg" +
                        (lastFocus ? " input-group-focus" : "")
                      }
                    >
                
                        <InputGroupText>
                          <i className="now-ui-icons text_caps-small"></i>
                        </InputGroupText>
                   
                      <Input
                        placeholder="Pasword..."
                        type="text"
                        onFocus={() => setLastFocus(true)}
                        onBlur={() => setLastFocus(false)}
                      ></Input>
                    </InputGroup>
                  </CardBody>
                  <CardFooter className="text-center">
                    <Button
                      block
                      className="btn-round"
                      color="info"
                      href="#pablo"
                      onClick={(e) => e.preventDefault()}
                      size="lg"
                    >
                      LOGIN
                    </Button>
                    <div className="pull-left">
                      <h6>
                        <a
                          className="link"
                          href="#pablo"
                          onClick={(e) => e.preventDefault()}
                        >
                          Create Account
                        </a>
                      </h6>
                    </div>
                    <div className="pull-right">
                      <h6>
                        <a
                          className="link"
                          href="#pablo"
                          onClick={(e) => e.preventDefault()}
                        >
                          Need Help?
                        </a>
                      </h6>
                    </div>
                  </CardFooter>
                </Form>
              </Card>
            </Col>
          </Container>
        </div>
      
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
  </Form>
      
  </>
);
  };
  export default Login;