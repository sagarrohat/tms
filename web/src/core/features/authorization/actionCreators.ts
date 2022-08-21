import {
  LOGIN_USER,
  SET_LOGIN_ALERT,
  LOGOUT_USER,
  SET_CURRENT_USER,
} from "./actionTypes";
import type { LoginResponse } from "../../types/user";

export const setLoginAlert = (message: string, type: string) => ({
  type: SET_LOGIN_ALERT,
  payload: {
    loginAlert: {
      message,
      type,
    },
  },
});

export const loginUser = (emailAddress: string, pd: string) => ({
  type: LOGIN_USER,
  payload: {
    emailAddress,
    pd,
  },
});

export const logoutUser = () => ({
  type: LOGOUT_USER,
});

export const setCurrentUser = (response: LoginResponse) => ({
  type: SET_CURRENT_USER,
  payload: {
    currentUser: {
      UserId: response.UserId,
      FirstName: response.FirstName,
      LastName: response.LastName,
      EmailAddress: response.EmailAddress,
      Secret: response.Secret,
      Expiry: response.Expiry,
    },
  },
});
