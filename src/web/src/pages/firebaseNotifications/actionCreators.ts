import { SET_MESSAGING_TOKEN } from "./actionTypes";

export const setMessagingToken = (token: string) => ({
  type: SET_MESSAGING_TOKEN,
  payload: {
    token,
  },
});
