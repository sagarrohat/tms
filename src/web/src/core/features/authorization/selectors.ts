import type { State } from "../../types/state";

export const isAuthenticated = (state: State) => state.user.isAuthenticated;

export const getLoginAlert = (state: State) => state.user.loginAlert;

export const getUserEmailAddress = (state: State) =>
  state.user.currentUser?.EmailAddress;

export const getJwtSecret = (state: State) => ({
  Secret: state.user.currentUser?.Secret,
  Expiry: state.user.currentUser?.Expiry,
});

export const getUser = (state: State) => ({
  FullName: `${state.user.currentUser?.FirstName} ${state.user.currentUser?.LastName}`,
  UserId: state.user.currentUser?.UserId,
});
