import { message } from "antd";
import React from "react";

type Props = {
  toasts: Array<Object>;
  hideToast: Function;
};

export function Toast(props: Props) {
  const { toasts, hideToast } = props;

  const renderToast = (toast: any) => {
    if (!toast) return null;

    var toastConfigs = {
      content: toast.message,
      duration: toast.duration,
      key: toast.id,
      onClose: hideToast(toast.id),
    };

    if (toast.type == "error") {
      return message.error(toastConfigs);
    } else if (toast.type == "info") {
      return message.info(toastConfigs);
    } else if (toast.type == "success") {
      return message.success(toastConfigs);
    } else if (toast.type == "warning") {
      return message.warning(toastConfigs);
    }

    return null;
  };

  return <div>{toasts && toasts.map((toast) => renderToast(toast))}</div>;
}

export default Toast;
