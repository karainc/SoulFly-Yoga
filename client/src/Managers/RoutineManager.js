const baseUrl = '/api/routine';

//Fetching every routine in the database
export const getAllRoutines = () => {
    return fetch(baseUrl).then((res) => res.json())
};


export const getRoutinesByUserId = (id) => {
    return fetch(`${baseUrl}/GetUsersRoutines/${id}`).then((res) => res.json())
}

export const getRoutineById = (id) => {
    return fetch(`/api/Routine/${id}`).then((res) => res.json())
}

//POST fetch to add a routine to the database
export const addRoutine = (singleRoutine) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(singleRoutine)
    });
}

//DELETE 
export const deleteRoutine = (id) => {
    return fetch(`/api/Routine/${id}`, {
      method: "DELETE",
    })
      .then(() => getAllRoutines())
  };


  export const updateRoutine = (routine) => {
    console.log(routine)
    return fetch(`/api/Routine/${routine.Id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(routine)
    }).then(() => getAllRoutines())
}