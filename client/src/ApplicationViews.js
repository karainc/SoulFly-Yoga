import { Route, Routes, Router } from "react-router-dom";
import { YogaCategories } from "./components/YogaCategories";
import React from "react";
import Login from "./components/Auth/Login";
import Register from "./components/Auth/Register";
import Authorize from "./components/Authorize";
import NavBar from "./components/Navbar/NavBar";


export const ApplicationViews = () => {

  return (
    
       
          <Routes>
         
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
    
            <Route path="*" element={
              <Authorize>
                <>
                  <NavBar />
                  <div className="content-container">
                        <div className="content-container">
                    <ApplicationViews />  
                  </div>
                    </div>
              </>
              </Authorize>
            } />
          </Routes>

        );
};