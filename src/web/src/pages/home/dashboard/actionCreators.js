import {
  FETCH_DASHBOARD_TASKS_BY_USERS,
  SET_DASHBOARD_TASKS_BY_USERS,
} from "./actionTypes";

export const fetchTasksByUsers = () => ({
  type: FETCH_DASHBOARD_TASKS_BY_USERS,
});

export const setTasksByUsers = (payload) => ({
  type: SET_DASHBOARD_TASKS_BY_USERS,
  payload,
});
