import React, { useEffect, useState } from "react";
import { Card, CardBody, CardTitle, CardText, Button, Alert } from "reactstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { deleteRoutine, getRoutineById } from "../Managers/RoutineManager";

export const RoutineDetails = () => {
  const [routine, setRoutine] = useState();
  const [showAlert, setShowAlert] = useState(false)
  const { id } = useParams();
  const navigate = useNavigate();
  const localSoulFlyUser = localStorage.getItem("users");
  const soulFlyUserObject = JSON.parse(localSoulFlyUser)


  useEffect(() => {
    getRoutineById(id).then(setRoutine);     
  }, []);

    if (!routine) {
        return null;
    }

  const handleDelete = () => {
    deleteRoutine(routine.id).then(() => {
      setShowAlert(false)
      navigate(`/routines`)
    });
  };

  
  const handleCancel = () => {
    setShowAlert(false) 
  }

  const deleteRoutineAlert = () => {
    return (<>
    <Alert color="danger" key={'danger'}>
      You really want to delete this session?
      <br></br><Link onClick={handleDelete}>Yes</Link> / <Link onClick={handleCancel}>No</Link>
    </Alert>
    </>)
  }

  //Delete button only shows for the user
  const deleteButtonForUser = () => {
    if (routine.userId === soulFlyUserObject.id) {
      return <><Button
      color="danger"
      type="delete"
      onClick={() => {
        setShowAlert(true);      
      }}> 
      Delete
    </Button>
      {showAlert && deleteRoutineAlert()}
      <p></p>
      <p></p>
      </>
    }
  }

  //Edit button only shows for the user
  const editRoutineButtonForUser = () => {
    if (routine.userId === soulFlyUserObject.id) {
      return <>
      <Button color="warning" onClick={() => navigate(`/routines/edit/${routine.id}`)}>Edit</Button>
      </>
  }}

  return (
    <div className="card-container">
    <Card style={{ width: '600px', height: 'auto', margin: '30px'}}>
        <CardTitle><b>The intention of this session is to:  {routine.intention}</b></CardTitle>
        <CardBody>
            <CardText>Number of Cycles:  {routine?.cycles}</CardText>
            <CardText>Name of Pose: {routine?.poses?.name}</CardText>
            <CardText>Pose Description: {routine?.poses?.description}</CardText>
            <CardText>Image of Pose: <img src={routine?.poses?.image} alt="#" style={{ height: '150px', objectFit: 'cover' }} /></CardText>
            <CardText>Relection: {routine?.reflection}</CardText>
            <CardText>
                Created on: {routine?.creationDate} by: <b>{routine?.users?.displayName}</b>
            </CardText>
            <Button onClick={() => navigate(`/reflections/${routine.id}`)}>Reflect</Button>
            <Button onClick={() => navigate(`/commentsById/${routine.id}`)}>View Comments</Button>
           |
            <Button onClick={() => navigate(`/addComment/${routine.id}`)}>Add Comment</Button>
            |
            {deleteButtonForUser()}|
            {editRoutineButtonForUser()}

        </CardBody>
    </Card>
    </div>
  );
};