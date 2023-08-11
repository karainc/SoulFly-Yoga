const apiUrl = "https://localhost:7145";

export const login = (userObject) => {
    return fetch(`${apiUrl}/api/Users/GetByEmail?email=${userObject.email}`)
    .then((r) => r.json())
      .then((users) => {
        if(users.id){
          localStorage.setItem("users", JSON.stringify(users));
          return users
        }
        else{
          return undefined
        }
      });
  };
  
  export const logout = () => {
        localStorage.clear()
  };
  
  export const register = (userObject, password) => {
    return  fetch(`${apiUrl}/api/Users`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(userObject),
    })
    .then((response) => response.json())
      .then((savedUsers) => {
        localStorage.setItem("users", JSON.stringify(savedUsers))
      });
  };