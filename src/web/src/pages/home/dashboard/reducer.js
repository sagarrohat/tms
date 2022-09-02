import {
  FETCH_DASHBOARD_TASKS_BY_USERS,
  SET_DASHBOARD_TASKS_BY_USERS,
} from "./actionTypes";

export const INITIAL_STATE = {
  tasksByUsers: undefined,
};

// A reducer is a pure function that takes an action and the *previous state* of the application
// and returns the new state.
export default (state = INITIAL_STATE, action = {}) => {
  switch (action.type) {
    case FETCH_DASHBOARD_TASKS_BY_USERS:
      return {
        ...state,
        tasksByUsers: undefined,
      };
    case SET_DASHBOARD_TASKS_BY_USERS:
      return {
        ...state,
        tasksByUsers: action.payload,
      };
    default:
      return state;
  }
};
