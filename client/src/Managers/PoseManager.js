import React from "react";

const apiUrl = "https://localhost:7063";

//Returns a list of all poses
export const getAllPoses = () => {
    return fetch("/api/Poses")
    .then((res) => res.json())
};

//Retrieves a single pose
export const getPoseById = (id) => {
    return fetch(`${apiUrl}/api/Poses/${id}`).then((r) => r.json());
}
