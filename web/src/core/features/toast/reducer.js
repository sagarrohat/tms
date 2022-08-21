import { SHOW_TOAST, HIDE_TOAST, CLEAR_TOASTS } from "./actionTypes";
import { LOGOUT_USER } from "../authorization/actionTypes";

const initialState = [];

export default (state: Array<Object> = initialState, action: Object) => {
  switch (action.type) {
    case SHOW_TOAST:
      return [action.payload, ...state];
    case HIDE_TOAST: {
      // .filter() won't mutate the source array here
      const stateAfterRemovingToast: Array<Object> = state.filter(
        (toast) => toast.id !== action.payload
      );
      return stateAfterRemovingToast;
    }
    case CLEAR_TOASTS:
    case LOGOUT_USER:
      return initialState;
    default:
      return state;
  }
};
