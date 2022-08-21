import {
  OPEN_TASK_EDITOR,
  CLOSE_TASK_EDITOR,
  SET_API_ERROR,
} from "./actionTypes";

export const INITIAL_STATE = {
  isOpen: false,
  item: {},
  apiError: null,
};

export default (state = INITIAL_STATE, action = {}) => {
  switch (action.type) {
    case OPEN_TASK_EDITOR:
      return {
        ...state,
        isOpen: true,
        item: action.payload || INITIAL_STATE.item,
        apiError: INITIAL_STATE.apiError,
      };
    case SET_API_ERROR:
      return {
        ...state,
        apiError: action.payload,
      };
    case CLOSE_TASK_EDITOR:
      return INITIAL_STATE;
    default:
      return state;
  }
};
