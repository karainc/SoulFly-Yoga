
export const getCategories = () => {
    return fetch(`https://yoga-api-nzy4.onrender.com/v1/categories`)
        .then(res => res.json())
        }
    
