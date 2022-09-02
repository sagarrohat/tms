// @flow
import {
  FETCH_ASSIGNABLE_USERS,
  SET_ASSIGNABLE_USERS,
  FETCH_TASKS,
  SET_TASKS,
  USER_CHANGED,
  TOGGLE_TAB,
} from "./actionTypes";

import { HOME_TAB_MYWORK } from "./constants";

export const INITIAL_STATE = {
  assignableUsers: [],
  activeTab: HOME_TAB_MYWORK,
};

export default (state = INITIAL_STATE, action = {}) => {
  switch (action.type) {
    case FETCH_ASSIGNABLE_USERS:
      return {
        ...state,
      };
    case SET_ASSIGNABLE_USERS:
      return {
        ...state,
        assignableUsers: action.payload,
      };
    case FETCH_TASKS:
      return {
        ...state,
        filters: { ...state.filters, ...action.payload?.filters },
      };
    case SET_TASKS:
      return {
        ...state,
        tasks: action.payload,
      };
    case USER_CHANGED:
      return {
        ...state,
        filters: { ...state.filters, ...action.payload?.filters },
      };
    case TOGGLE_TAB:
      return {
        ...state,
        activeTab: action.payload,
      };
    default:
      return state;
  }
};
