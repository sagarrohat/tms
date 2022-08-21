import {
  TASK_CREATE_OR_UPDATE,
  OPEN_TASK_EDITOR,
  CLOSE_TASK_EDITOR,
  SET_API_ERROR,
  DELETE_TASK,
} from "./actionTypes";

export const taskCreateOrUpdate = (item, directUpdate) => {
  return {
    type: TASK_CREATE_OR_UPDATE,
    payload: {
      item,
      directUpdate,
    },
  };
};

export const setApiError = (payload) => {
  return {
    type: SET_API_ERROR,
    payload,
  };
};

export const taskDelete = (payload) => {
  return {
    type: DELETE_TASK,
    payload,
  };
};

export const editorOpen = (payload) => {
  return {
    type: OPEN_TASK_EDITOR,
    payload,
  };
};

export const editorClose = (payload) => {
  return {
    type: CLOSE_TASK_EDITOR,
    payload,
  };
};
