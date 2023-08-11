const baseUrl = '/api/Routine';

//Fetching every routine in the database
export const getAllRoutines = () => {
    return fetch(baseUrl).then((res) => res.json())
};

//Fetching only routines made by a user in the database. The "id" parameter is essential because we connect this with the user's id through localStorage
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

//PUT fetch to edit the individual post. Selecting it by it's id
  export const editRoutine = (routine) => {
    console.log(routine)
    return fetch(`/api/Routine/${routine.Id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(routine)
    }).then(() => getAllRoutines())
}