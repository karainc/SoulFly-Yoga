import React from "react";
import { Card, Container, CardImg, CardTitle, CardText, CardBody } from "reactstrap";
import { Link } from "react-router-dom";

export const Poses = ({ poses }) => {

  return (
  <>
    <Container className="poses-container">
   

       <Card height="30%" width="25%">
        <CardImg width='100%' src={poses.url_svg}/>
        <CardBody>
        <CardTitle>{poses.english_name} /{poses.sanskrit_name}</CardTitle>
        
        
        <CardText>Pose Benefits: {poses.pose_benefits}</CardText>
        </CardBody>
    </Card>


    </Container>
    
   
    </>
  );
  }