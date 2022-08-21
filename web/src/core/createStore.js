/* eslint-disable global-require */
/* eslint-disable import/no-extraneous-dependencies */
/* eslint-disable prefer-destructuring */

import { createStore, applyMiddleware } from "redux";
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

const store = createStore(persistedReducer, applyMiddleware(...middlewareList));

sagaMiddleware.run(rootSaga);

const persistor = persistStore(store);

export { store, persistor };
