import { useEffect, useState } from "react"
import { getAllRoutines } from "../Managers/RoutineManager";
import { Routine } from "./Routine";
import { Table, Col, Button } from "reactstrap";
import { useNavigate } from "react-router-dom";


export const RoutineList = () => {
    const [routines, setRoutines] = useState([]);
    const navigate = useNavigate()

    const getRoutines = () => {
        getAllRoutines().then(allRoutines => setRoutines(allRoutines));
    }

    useEffect(() => {
        getRoutines();
    }, [])

    return (<>
      <div className="routine-list">
       
            <p></p><Col><h1>Let's Get Started!</h1></Col>
              <Col>Need Help getting started? Checkout this short 10 minute video and come back to create your very own session! <iframe width="500" height="315" src="https://www.youtube.com/embed/v7AYKMP6rOE?si=wkXabi-iSUjonblN" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
              </Col>
              <p></p>
     <Button
            onClick={() => navigate("/routines/add")}
            
            
            className="create-routine-button">
                Create Routine
            </Button>
            <p></p>
              <Table dark> 
            <thead>
              <tr>
                <th>Intention</th>
                <th>User</th>
                <th>Cycles</th>
                <th>Poses</th>
                <th>Reflection</th>
                <th>More</th>
              </tr>
            </thead>
              {routines.map((routine) => {
                return  <Routine key={routine.id} routine={routine} />
              })}
            </Table>
          </div>
      

    
    </>
    )
}



 