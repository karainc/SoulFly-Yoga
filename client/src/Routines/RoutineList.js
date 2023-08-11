import { useEffect, useState } from "react"
import { getAllRoutines } from "../Managers/RoutineManager";
import { Routine } from "./Routine";
import { Table } from "reactstrap";

export const RoutineList = () => {
    const [routines, setRoutines] = useState([]);

    const getRoutines = () => {
        getAllRoutines().then(allRoutines => setRoutines(allRoutines));
    }

    useEffect(() => {
        getRoutines();
    }, [])

    return (<>
      <div className="routine-list">
        <div className="row justify-content-center">
          <div className="cards-column">
            <Table> 
            <thead>
              <tr>
                <th>Intention</th>
                <th>Cycles</th>
                <th>Poses</th>
                <th>Reflection</th>
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



 