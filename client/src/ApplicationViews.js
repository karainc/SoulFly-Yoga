import { Route, Routes } from "react-router-dom";
import React from "react";
import Login from "./components/Auth/Login";
import Hello from "./Hello";
import { RoutineDetails } from "./Routines/RoutineDetails";
import { RoutineEdit } from "./Routines/RoutineEdit";
import { YogaPoses } from "./Pages/Library.js";
import { RoutineList } from "./Routines/RoutineList";
import { RoutineForm } from "./Routines/RoutineForm";
// import { YogaCategories } from "./components/YogaCategories";
import { CommentList } from "./Comments/CommentList.js";
import { CommentForm } from "./Comments/CommentForm.js";
import { CommentEdit } from "./Comments/CommentEdit.js";

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
        <Route path="/library" element={<YogaPoses />} />
        {/* <Route path="/library" element={<YogaCategories />} /> */}
        <Route path="comment/:id" element={<CommentList />} />
        <Route path="comment/create/:postId" element={<CommentForm />} />
        <Route path="comment/edit/:commentId" element={<CommentEdit/>} />

          
          </Routes>
          

        );
};