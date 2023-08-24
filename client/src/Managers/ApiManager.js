
export const getAllPoses = () => {
    return fetch(`https://yoga-api-nzy4.onrender.com/v1/poses`)
        .then(res => res.json())
        }
    
        // export const getCategories = () => {
        //     return fetch(`https://yoga-api-nzy4.onrender.com/v1/categories`)
        //         .then(res => res.json())
        //         }
            
