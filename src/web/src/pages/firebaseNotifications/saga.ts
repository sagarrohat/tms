// @flow
import { takeEvery, call, put } from "redux-saga/effects";
import { executeApiCall } from "../../core/sagas/api";
import type { ApiOptions, ApiResponse } from "../../core/sagas/api";
import { addToast } from "../../core/features/toast/actionCreators";
import { messagingConfigurationApi } from "../../core/endpoints";
import { SET_MESSAGING_TOKEN } from "./actionTypes";

export default function* firebaseNotificationsSaga() {
  yield takeEvery(SET_MESSAGING_TOKEN, setMessagingToken);
}

export function* setMessagingToken(action: any) {
  const { token } = action.payload;

  const apiOptions: ApiOptions = {
    url: `${messagingConfigurationApi}?token=${token}`,
    method: "POST",
  };

  const apiResponse: ApiResponse = yield call(executeApiCall, apiOptions);

  const { isSuccessful, response = {} } = apiResponse;

  if (!isSuccessful) {
    yield put(addToast({ message: response.ErrorMessage, type: "error" }));
  }
}
