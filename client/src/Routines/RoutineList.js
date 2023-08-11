import { useEffect, useState } from "react"
import { getAllRoutines } from "../Managers/RoutineManager";
import { Routine } from "./Routine";
import { Table, Button } from "reactstrap";
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
     <Button
            onClick={() => navigate("/routines/add")}
            
            
            className="btn btn-primary">
                Create Routine
            </Button>
      <div className="routine-list">
        <div className="row justify-content-center">
          <div className="cards-column">
            <Table> 
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
        </div>
      </div>
    
    </>
    )
}



 