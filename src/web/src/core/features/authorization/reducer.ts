import { UserState } from "../../types/state";
import {
  LOGIN_USER,
  SET_LOGIN_ALERT,
  LOGOUT_USER,
  SET_CURRENT_USER,
} from "./actionTypes";

export const INITIAL_STATE: UserState = {
  isAuthenticated: false,
  currentUser: null,
  loginAlert: null,
};

// A reducer is a pure function that takes an action and the *previous state* of the application
// and returns the new state.
export default (state = INITIAL_STATE, action: any): UserState => {
  switch (action.type) {
    case LOGIN_USER:
      return {
        ...state,
        loginAlert: undefined,
      };
    case SET_LOGIN_ALERT:
      return {
        ...state,
        ...action.payload,
      };
    case SET_CURRENT_USER:
      return {
        ...state,
        ...action.payload,
        isAuthenticated: true,
      };
    case LOGOUT_USER:
      return {
        ...state,
        currentUser: undefined,
        isAuthenticated: false,
      };
    default:
      return state;
  }
};
