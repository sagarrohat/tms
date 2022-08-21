import { takeEvery, call, put, select } from "redux-saga/effects";
import { executeApiCall } from "../../core/sagas/api";
import type { ApiOptions, ApiResponse } from "../../core/sagas/api";
import { TASK_CREATE_OR_UPDATE, DELETE_TASK } from "./actionTypes";
import { fetchTasks } from "../home/actionCreators";
import { editorClose, setApiError } from "./actionCreators";
import { tasksApi } from "../../core/endpoints";
import { addToast } from "../../core/features/toast/actionCreators";
import { getActiveTab } from "../home/selectors";
import { HOME_TAB_MYWORK } from "../home/constants";

export default function* taskEditorSaga(): Generator<any, any, any> {
  yield takeEvery(TASK_CREATE_OR_UPDATE, taskCreateOrUpdate);
  yield takeEvery(DELETE_TASK, taskDelete);
}

export function* taskCreateOrUpdate(action: any): Generator<any, any, any> {
  const activeTab = yield select(getActiveTab);
  const { item, directUpdate } = action.payload;

  const getParams = () => {
    if (item?.Id) {
      item.Id = undefined;
      return item;
    }
    return item;
  };

  const apiOptions: ApiOptions = {
    url: item?.Id ? `${tasksApi}\\${item?.Id}` : tasksApi,
    method: item?.Id ? "PUT" : "POST",
    params: getParams(),
  };

  const apiResponse: ApiResponse = yield call(executeApiCall, apiOptions);

  const { isSuccessful, response = {} } = apiResponse;

  if (isSuccessful) {
    if (!directUpdate) yield put(editorClose());
    if (activeTab == HOME_TAB_MYWORK) yield put(fetchTasks(null)); // Use filters which are already in state
  } else {
    if (directUpdate) {
      yield put(addToast({ message: response.ErrorMessage, type: "error" }));
    } else {
      yield put(setApiError(response.ErrorMessage));
    }
  }
}

export function* taskDelete(action: any): Generator<any, any, any> {
  const { id } = action.payload;

  const apiOptions: ApiOptions = {
    url: `${tasksApi}/${id}`,
    method: "DELETE",
  };

  const apiResponse: ApiResponse = yield call(executeApiCall, apiOptions);

  const { isSuccessful, response = {} } = apiResponse;

  if (isSuccessful) {
    yield put(fetchTasks(null)); // Use filters which are already in state
  } else {
    yield put(addToast({ message: response.ErrorMessage, type: "error" }));
  }
}
