import { Link } from "react-router-dom"

export const Categories = ({ id, categories_name }) => {
    return <section className="categories">
        <div>
            <Link to={`/categories/${id}`}>Name: {categories_name}</Link>
        </div>
    </section> 
}