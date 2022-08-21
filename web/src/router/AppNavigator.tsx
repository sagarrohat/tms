import { Routes, Route, Navigate } from "react-router-dom";
import Home from "../pages/home/Home";
import Login from "../pages/login/Login";
import { Authorized } from "./Authorized";
import { Anonymous } from "./Anonymous";
import React from "react";

function AppNavigator() {
  return (
    <Routes>
      <Route path="/login" element={<Anonymous component={Login} />} />
      <Route path="/" element={<Navigate to="/home" />} />
      <Route path="/home" element={<Authorized component={Home} />} />
    </Routes>
  );
}

export default AppNavigator;
