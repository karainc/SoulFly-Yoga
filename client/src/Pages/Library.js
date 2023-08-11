import React, { useEffect, useState } from 'react';
import { getPoses } from '../Managers/ApiManager';


export const YogaPoses = () => {
  const [poses, setPoses] = useState([]);

  

  
  useEffect( 
    () => {
    getPoses()
    .then((posesArray) => setPoses(posesArray))  
    }, []
  )


    return(
        <>
            {poses.map((pose) => (
                <div className="inner-pose" key={pose.id}>
                <h3 className="pose-name">{poses.poses_name}</h3>
                  </div>  
            ))}
      </>
    ) 
}