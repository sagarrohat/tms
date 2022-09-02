import {
  LOGIN_USER,
  SET_LOGIN_ALERT,
  LOGOUT_USER,
  SET_CURRENT_USER,
} from "./actionTypes";
import type { LoginResponse, LoginAlert, LoginRequest } from "./types";

export const setLoginAlert = (loginAlert: LoginAlert) => ({
  type: SET_LOGIN_ALERT,
  payload: {
    loginAlert,
  },
});

export const loginUser = (loginRequest: LoginRequest) => ({
  type: LOGIN_USER,
  payload: loginRequest,
});

export const logoutUser = () => ({
  type: LOGOUT_USER,
});

export const setCurrentUser = (response: LoginResponse) => ({
  type: SET_CURRENT_USER,
  payload: {
    currentUser: response,
  },
});
