// @flow
import { useSelector } from "react-redux";
import { Navigate } from "react-router-dom";
import { isAuthenticated } from "../core/features/authorization/selectors";
import React from "react";

export const Anonymous: React.FC<any> = ({ component: RouteComponent }) => {
  const redirect = useSelector(isAuthenticated);

  if (redirect) {
    return <Navigate to="/home" />;
  }

  return <RouteComponent />;
};
