import React, { useEffect, useState } from 'react';
import { getCategories } from './ApiManager';


export const YogaCategories = () => {
  const [categories, setCategories] = useState([]);

  useEffect( 
    () => {
    getCategories()
    .then((categoryArray) => setCategories(categoryArray))  
    }, []
  )


    return(
        <>
            {categories.map((category) => (
                <div className="inner-category" key={category.id}>
                <h3 className="category-name">{category.category_name}</h3>
                  </div>  
            ))}
      </>
    ) 
}