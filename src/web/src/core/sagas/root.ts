// @flow
import { all, fork } from "redux-saga/effects";
import homeSaga from "../../pages/home/saga";
import loginSaga from "../features/authorization/saga";
import taskEditorSaga from "../../pages/taskEditor/saga";
import dashboardSaga from "../../pages/home/dashboard/saga";
import toastSaga from "../features/toast/saga";
import firebaseNotificationsSaga from "../../pages/firebaseNotifications/saga";

export default function* rootSaga() {
  yield all([
    fork(homeSaga),
    fork(loginSaga),
    fork(taskEditorSaga),
    fork(dashboardSaga),
    fork(toastSaga),
    fork(firebaseNotificationsSaga),
  ]);
}
