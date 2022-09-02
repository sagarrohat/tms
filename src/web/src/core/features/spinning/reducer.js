import { UPDATE_SPINNING_STATE } from "./actionTypes";

export const INITIAL_STATE = {
  isSpinning: false,
};

// A reducer is a pure function that takes an action and the *previous state* of the application
// and returns the new state.
export default (state = INITIAL_STATE, action = {}) => {
  switch (action.type) {
    case UPDATE_SPINNING_STATE:
      return {
        ...state,
        isSpinning: action.payload,
      };
    default:
      return state;
  }
};
