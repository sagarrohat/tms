export const isAuthenticated = (state) => state.user.isAuthenticated;

export const getLoginAlert = (state) => state.user.loginAlert;

export const getUserEmailAddress = (state) =>
  state.user.currentUser?.EmailAddress;

export const getJwtSecret = (state) => ({
  Secret: state.user.currentUser?.Secret,
  Expiry: state.user.currentUser?.Expiry,
});

export const getUser = (state) => ({
  FullName: `${state.user.currentUser?.FirstName} ${state.user.currentUser?.LastName}`,
  UserId: state.user.currentUser?.UserId,
});
