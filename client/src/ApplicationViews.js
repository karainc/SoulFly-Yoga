import { Route, Routes } from "react-router-dom";
import React from "react";
import Login from "./components/Auth/Login";
import Hello from "./Hello";
import { RoutineList } from "./Routines/RoutineList";
// import PosesList from "./Poses/PosesList";

// import { UserRoutines } from "./Routines/UserRoutines";
import { RoutineDetails } from "./Routines/RoutineDetails";
// import { CommentList } from "./Comments/CommentList.js";
import { RoutineForm } from "./Routines/RoutineForm";
import { RoutineEdit } from "./Routines/RoutineEdit";
// import { CommentForm } from "./Comments/CommentForm.js";
import { YogaPoses } from "./Pages/Library";



export const ApplicationViews = () => {

  return (
    
       
          <Routes>
        <Route path="/" element={<Hello />} />
        <Route path="/routines" element={<RoutineList />} />
        <Route path="/routines/:id" element={<RoutineDetails />} />
        <Route path="/routines/add" element={<RoutineForm />} />
        <Route path="/routines/edit/:routineId" element={<RoutineEdit />} />
        {/* <Route path="/myRoutines" element={<UserRoutines />} /> */}
        <Route path="/login" element={<Login />} />
        {/* <Route path="/poses" element={<PoseList />} /> */}
        <Route path="/library" element={<YogaPoses />} />

          
          </Routes>

        );
};