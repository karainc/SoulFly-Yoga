import { Route, Routes } from "react-router-dom";
import { YogaCategories } from "./YogaCategories";

export const ApplicationViews = () => {

    return (
        <Routes>
            <Route path="/" element={
                <>
                <h1>SoulFly Yoga</h1>
                <div>Your Yoga Experience</div>
                <YogaCategories />
                </>
            }
            />
        </Routes>
    )
}