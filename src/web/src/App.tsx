import { Provider } from "react-redux";
import { PersistGate } from "redux-persist/integration/react";
import { HashRouter } from "react-router-dom";
import RootContainer from "./RootContainer";
import { store, persistor } from "./core/createStore";
import React from "react";
require('dotenv').config()

function App() {
  return (
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <HashRouter>
          <RootContainer />
        </HashRouter>
      </PersistGate>
    </Provider>
  );
}

export default App;
