// @flow
import { all, fork } from "redux-saga/effects";
import type { Saga } from "redux-saga";
import homeSaga from "../../pages/home/saga";
import loginSaga from "../features/authorization/saga";
import taskEditorSaga from "../../pages/taskEditor/saga";
import dashboardSaga from "../../pages/home/dashboard/saga";
import toastSaga from "../features/toast/saga";

export default function* rootSaga(): Saga<void> {
  yield all([
    fork(homeSaga),
    fork(loginSaga),
    fork(taskEditorSaga),
    fork(dashboardSaga),
    fork(toastSaga),
  ]);
}
