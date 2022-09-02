import { LoginAlert, LoginResponse } from "../features/authorization/types";

export interface State {
  user: UserState;
}

export interface UserState {
  isAuthenticated: boolean;
  currentUser: LoginResponse;
  loginAlert: LoginAlert;
}
