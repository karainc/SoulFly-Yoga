import React from "react";
import { Link } from "react-router-dom";

//In charge of each Routine
export const Routine = ({ routine }) => {

  return (
      <tbody>
        <tr>
          <td>{routine.intention}</td>
          <td>{`${routine.users.displayName} ${routine.poses.id}`}</td>
          <td><Link to={`/routines/${routine.id}`}>Details</Link></td>
        </tr>
      </tbody>
  );
};