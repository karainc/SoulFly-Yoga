import React, { useEffect, useState } from 'react';
import { getAllPoses } from '../Managers/ApiManager';
import { Card, Col, Row, Container, CardBody, CardImg, CardImgOverlay, CardTitle, CardText, } from 'reactstrap';
import { Poses } from '../Pages/LibraryCard';

export const YogaPoses = () => {
  const [poses, setPoses] = useState([]);
  // const navigate = useNavigate();

  const getPoses = () => {
    getAllPoses().then(allPoses => setPoses(allPoses));
  };

  
  useEffect(() => {
      getPoses();
    }, []);
    
    return(
        <><>
       <p></p>
          <Card className='overlay' inverse>
        <CardImg
          alt="Card image cap"
          src="https://picsum.photos/id/82/600/170"
          style={{
            height: 150
          }}
          width="80%" />
        <CardImgOverlay>
          <CardTitle className="overlay-title" tag="h1">
            Yoga Library
          </CardTitle>
          <p></p>
          <CardText className='deeper'>
            Your resource for a more in depth exploration into poses!
          </CardText>
        </CardImgOverlay>
      </Card>
      </><>
          <Container className="pose-container">
            <Row sm="3">
              {poses.map((pose) => {
                return <Col><Poses key={poses.id} poses={pose} /></Col>;
              })}
            </Row>
          </Container>
        </></>
        );
        };

