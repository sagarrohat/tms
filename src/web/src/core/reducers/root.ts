import { combineReducers } from "redux";
import home from "../../pages/home/reducer";
import taskEditor from "../../pages/taskEditor/reducer.js";
import user from "../features/authorization/reducer";
import dashboard from "../../pages/home/dashboard/reducer";
import spinning from "../features/spinning/reducer";
import toast from "../features/toast/reducer";

const rootReducer = combineReducers({
  home,
  user,
  taskEditor,
  dashboard,
  spinning,
  toast,
});

export default rootReducer;
