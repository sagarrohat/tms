// @flow
import { takeEvery, call, put, select } from "redux-saga/effects";
import { executeApiCall } from "../../../core/sagas/api";
import type { Saga } from "redux-saga";
import { FETCH_DASHBOARD_TASKS_BY_USERS } from "./actionTypes";
import { setTasksByUsers } from "./actionCreators";
import { addToast } from "../../../core/features/toast/actionCreators";
import { dashboardApi } from "../../../core/endpoints";
import { getTaskListFilters } from "../selectors";

export default function* dashboardSaga(): Saga<void> {
  yield takeEvery(FETCH_DASHBOARD_TASKS_BY_USERS, fetchTasksByUsers);
}

export function* fetchTasksByUsers(action: Object): Saga<void> {
  const filters = yield select(getTaskListFilters);

  const apiOptions: ApiOptions = {
    url: `${dashboardApi}/tasks`,
    method: "GET",
    useJwtSecret: true,
    params: { AssignedUserId: filters?.AssignedUserId },
  };

  const apiResponse: ApiResponse = yield call(executeApiCall, apiOptions);

  const { isSuccessful, response = {} } = apiResponse;

  if (isSuccessful) {
    yield put(setTasksByUsers(response));
  } else {
    yield put(addToast({ message: response.ErrorMessage, type: "error" }));
  }
}
