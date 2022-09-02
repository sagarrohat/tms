import { configureStore } from "@reduxjs/toolkit";
import createSagaMiddleware from "redux-saga";
import { persistStore, persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";
import { createFilter } from "redux-persist-transform-filter";
import rootReducer from "./reducers/root";
import rootSaga from "./sagas/root";

const userFilter = createFilter("user", ["isAuthenticated", "currentUser"]);
const homeFilter = createFilter("home", ["assignableUsers"]);

const persistConfig = {
  key: "root",
  storage: storage,
  whitelist: ["home", "user"],
  transforms: [homeFilter, userFilter],
};

const persistedReducer = persistReducer(persistConfig, rootReducer);

const sagaMiddleware = createSagaMiddleware();

const middlewareList = [sagaMiddleware];

const store = configureStore({
  reducer: persistedReducer,
  devTools: process.env.NODE_ENV !== "production",
  middleware: middlewareList,
});

sagaMiddleware.run(rootSaga);

const persistor = persistStore(store);

export { store, persistor };
