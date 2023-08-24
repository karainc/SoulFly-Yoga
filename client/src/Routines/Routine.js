import React from "react";
import { Link } from "react-router-dom";

//In charge of each Routine
export const Routine = ({ routine }) => {
    console.log(routine)
  return (
    
      <tbody>
        <tr>
          <td> {routine.intention}</td>
          <td>{`${routine?.users?.displayName}`}</td>
          <td>{`${routine.cycles}`}</td>
          <td>{`${routine?.poses?.name}`}</td>
          <td>{`${routine.reflection}`}</td>
          <td><Link to={`/routines/${routine.id}`}>Details</Link></td>
        </tr>
      </tbody>
  );
};