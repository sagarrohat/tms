// @flow
import {
  FETCH_ASSIGNABLE_USERS,
  SET_ASSIGNABLE_USERS,
  FETCH_TASKS,
  SET_TASKS,
  PIN_TASK,
  UNPIN_TASK,
  USER_CHANGED,
  TOGGLE_TAB,
} from "./actionTypes";
import type { TaskResponse } from "../../core/types/task";
export const setAssignableUsers = (payload: String[]) => {
  return {
    type: SET_ASSIGNABLE_USERS,
    payload,
  };
};

export const fetchAssignableUsers = () => ({
  type: FETCH_ASSIGNABLE_USERS,
});

export const setTasks = (payload: TaskResponse[]) => {
  return {
    type: SET_TASKS,
    payload,
  };
};

export const fetchTasks = (payload) => {
  return {
    type: FETCH_TASKS,
    payload,
  };
};

export const pinTask = (payload) => {
  return {
    type: PIN_TASK,
    payload,
  };
};

export const unpinTask = (payload) => {
  return {
    type: UNPIN_TASK,
    payload,
  };
};

export const userChanged = (payload) => {
  return {
    type: USER_CHANGED,
    payload,
  };
};

export const toggleTab = (payload) => {
  return {
    type: TOGGLE_TAB,
    payload
  };
};
