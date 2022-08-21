import React from "react";
import { ADD_TOAST, SHOW_TOAST, HIDE_TOAST, CLEAR_TOASTS } from "./actionTypes";

export type ToastConfig = {
  id?: string,
  message: string | React.Component<any, any> | Function,
  duration?: number,
  type?: "success" | "error" | "info" | "warning",
};

export const addToast = ({
  id,
  message,
  duration = 0.5,
  type = "info",
}: ToastConfig) => ({
  type: ADD_TOAST,
  payload: {
    id,
    message,
    duration,
    type,
  },
});

type ShowToastConfig = {
  id: string,
  message: string | React.Component<any, any> | Function,
};

export const showToast = (toastConfig: ShowToastConfig) => ({
  type: SHOW_TOAST,
  payload: toastConfig,
});

export const hideToast = (toastId: string) => ({
  type: HIDE_TOAST,
  payload: toastId,
});

export const clearToasts = () => ({
  type: CLEAR_TOASTS,
});
