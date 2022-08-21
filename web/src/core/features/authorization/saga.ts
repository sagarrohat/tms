// @flow
import { takeEvery, call, put } from "redux-saga/effects";
import { executeApiCall } from "../../sagas/api";
import type { ApiOptions, ApiResponse } from "../../sagas/api";
import { LOGIN_USER } from "./actionTypes";
import { setCurrentUser, setLoginAlert } from "./actionCreators";
import { fetchAssignableUsers } from "../../../pages/home/actionCreators";
import { loginApi } from "../../endpoints";

export default function* loginSaga(): Generator<any, any, any> {
  yield takeEvery(LOGIN_USER, login);
}

export function* login(action: any): Generator<any, any, any> {
  const { emailAddress, pd } = action.payload;

  const apiOptions: ApiOptions = {
    url: loginApi,
    method: "POST",
    params: {
      EmailAddress: emailAddress,
      Pd: pd,
    },
    useJwtSecret: false,
  };

  const apiResponse: ApiResponse = yield call(executeApiCall, apiOptions);

  const { isSuccessful, response = {} } = apiResponse;

  if (isSuccessful) {
    yield put(setCurrentUser(response));
    yield put(fetchAssignableUsers());
  } else {
    yield put(setLoginAlert(response.ErrorMessage, "error"));
  }
}
