// @flow
import { takeEvery, call, put, select } from "redux-saga/effects";
import { executeApiCall } from "../../core/sagas/api";
import type { ApiResponse, ApiOptions } from "../../core/sagas/api";
import {
  FETCH_ASSIGNABLE_USERS,
  FETCH_TASKS,
  PIN_TASK,
  UNPIN_TASK,
  TOGGLE_TAB,
  USER_CHANGED,
} from "./actionTypes";
import { usersApi, tasksApi } from "../../core/endpoints";
import { setAssignableUsers, setTasks } from "./actionCreators";
import { addToast } from "../../core/features/toast/actionCreators";
import { fetchTasksByUsers } from "./dashboard/actionCreators";
import { getTaskListFilters, getActiveTab } from "./selectors";
import { HOME_TAB_DASHBOARD, HOME_TAB_MYWORK } from "./constants";
export default function* homeSaga() {
  yield takeEvery(FETCH_ASSIGNABLE_USERS, fetchAssignableUsers);
  yield takeEvery(FETCH_TASKS, fetchTasks);
  yield takeEvery(PIN_TASK, pinTask);
  yield takeEvery(UNPIN_TASK, unpinTask);
  yield takeEvery(TOGGLE_TAB, toggleTab);
  yield takeEvery(USER_CHANGED, toggleTab);
}

export function* toggleTab() {
  const activeTab = yield select(getActiveTab);
  if (activeTab == HOME_TAB_DASHBOARD) {
    yield put(fetchTasksByUsers());
  } else if (activeTab == HOME_TAB_MYWORK) {
    yield call(fetchTasks);
  }
}

export function* fetchAssignableUsers() {
  const apiOptions: ApiOptions = {
    url: usersApi,
    method: "GET",
  };

  const apiResponse: ApiResponse = yield call(executeApiCall, apiOptions);

  const { isSuccessful, response = {} } = apiResponse;

  if (isSuccessful) {
    yield put(setAssignableUsers(response));
  }
}

export function* fetchTasks() {
  const filters = yield select(getTaskListFilters);

  const apiOptions: ApiOptions = {
    url: tasksApi,
    method: "GET",
    params: filters,
  };

  const apiResponse: ApiResponse = yield call(executeApiCall, apiOptions);

  const { isSuccessful, response = {} } = apiResponse;

  if (isSuccessful) {
    yield put(setTasks(response));
  } else {
    yield put(addToast({ message: response.ErrorMessage, type: "error" }));
  }
}

export function* pinTask(action: any) {
  const { id } = action.payload;

  const apiOptions: ApiOptions = {
    url: `${tasksApi}/${id}/pinned`,
    method: "POST",
  };

  const apiResponse: ApiResponse = yield call(executeApiCall, apiOptions);

  const { isSuccessful, response = {} } = apiResponse;

  if (isSuccessful) {
    yield call(fetchTasks); // Use filters which are already in state
  } else {
    yield put(addToast({ message: response.ErrorMessage, type: "error" }));
  }
}

export function* unpinTask(action: any) {
  const { id } = action.payload;

  const apiOptions: ApiOptions = {
    url: `${tasksApi}/${id}/pinned`,
    method: "DELETE",
  };

  const apiResponse: ApiResponse = yield call(executeApiCall, apiOptions);

  const { isSuccessful, response = {} } = apiResponse;

  if (isSuccessful) {
    yield call(fetchTasks); // Use filters which are already in state
  } else {
    yield put(addToast({ message: response.ErrorMessage, type: "error" }));
  }
}
