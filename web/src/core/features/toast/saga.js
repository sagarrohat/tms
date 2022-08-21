import { takeEvery, put, call } from "redux-saga/effects";
import { delay } from "redux-saga/effects";
import type { Saga } from "redux-saga";
import { hideToast, showToast } from "./actionCreators";
import { ADD_TOAST } from "./actionTypes";
import { generateRandomId } from "./utils";

export default function* toastSaga(): Saga<void> {
  yield takeEvery(ADD_TOAST, handleToast);
}

export function* handleToast(action: Object): Saga<void> {
  const { id, message, duration, type } = action.payload;

  let toastId = id;
  if (toastId === undefined) {
    toastId = yield call(generateRandomId);
  }

  yield put(
    showToast({
      id: toastId,
      message,
      type,
    })
  );

  yield delay(duration * 1000);
  yield put(hideToast(toastId));
}
