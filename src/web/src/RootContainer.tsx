import React from "react";
import { useSelector } from "react-redux";
import { Spin } from "antd";
import AppNavigator from "./router/AppNavigator";
import { Toast } from "./components";
import { isSpinning } from "./core/features/spinning/selectors";
import { getToasts } from "./core/features/toast/selectors";
import { hideToast } from "./core/features/toast/actionCreators";

function RootContainer() {
  var spinning = useSelector(isSpinning);
  var toasts = useSelector(getToasts);

  return (
    <div style={{ background: "#f9f9f9 !important" }}>
      <Toast toasts={toasts} hideToast={hideToast} />
      <Spin style={{ zIndex: "1001" }} spinning={spinning} tip="Loading...">
        <AppNavigator />
      </Spin>
    </div>
  );
}

export default RootContainer;
