
export const getPoses = () => {
    return fetch(`https://yoga-api-nzy4.onrender.com/v1/poses`)
        .then(res => res.json())
        }
    
