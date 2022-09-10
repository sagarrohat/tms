import moment from "moment";
import { TaskResponse } from "../../core/types/task";

export function convertToFormFields(taskItem: TaskResponse) {
  let result: any = {
    DueDate: moment.utc(taskItem?.DueDate),
    Title: taskItem?.Title,
    PercentageCompleted: taskItem.PercentageCompleted,
    Description: taskItem.Description,
    Status: taskItem.Status,
    AssignedUserId: taskItem.AssignedUserId,
  };

  if (taskItem?.Priority) {
    if (taskItem.Priority === 1) {
      result.Priority = "Low";
    } else if (taskItem.Priority === 2) {
      result.Priority = "Normal";
    } else if (taskItem.Priority === 3) {
      result.Priority = "High";
    }
  } else {
    result.Priority = "Normal";
  }

  return result;
}

export function convertFromFormFields(taskItem: TaskResponse, values: any) {
  let priority = Number(values.Priority);
  if (isNaN(priority)) {
    if (values.Priority == "Low") {
      priority = 1;
    } else if (values.Priority == "Normal") {
      priority = 2;
    } else if (values.Priority == "High") {
      priority = 3;
    }
  }
  return {
    Id: taskItem.Id,
    Title: values.Title,
    Description: values.Description,
    Status: taskItem.Status,
    Priority: priority,
    AssignedUserId: values.AssignedUserId,
    DueDate: moment.utc(values.DueDate),
    PercentageCompleted: values.PercentageCompleted,
  };
}
