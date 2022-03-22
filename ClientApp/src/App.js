import React, { useEffect, useState } from "react";

import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import NotifMessageQueue from "./components/NotifMessageQueue";
import Navigation from "./components/Navigation";
import { authReducer } from "./services/AuthReducer";
import { messageReducer, PUSH_MESSAGE } from "./services/MessageReducer";
import { SuperUserRoutes } from "./routes/SuperUserRoutes";
import { NormalUserRoutes } from "./routes/NormalUserRoutes";
import "./App.css";
import { AppRoutes } from "./routes/AppRoutes";

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");

const initialMessageState = {
  messages: [],
};
const initialAuthState = {
  isAuthenticated: false,
  tokenKey: null,
  userId: null,
  userName: null,
  accessLevel: null,
  expireTime: null,
};
export const MessageContext = React.createContext(initialMessageState);
export const AuthContext = React.createContext(initialAuthState);

function App() {
  const [messageState, messageDispatch] = React.useReducer(
    messageReducer,
    initialMessageState
  );
  const [authState, authDispatch] = React.useReducer(
    authReducer,
    initialAuthState
  );
  const [routes, setRoutes] = useState(AppRoutes);

  useEffect(() => {
    var timer;
    if (authState.isAuthenticated === true) {
      if (authState.accessLevel === "Super") {
        setRoutes(SuperUserRoutes);
      } else if (authState.accessLevel === "Normal") {
        setRoutes(NormalUserRoutes);
      }
      timer = setTimeout(() => {
        authDispatch({ type: "LOGOUT_SESSION" });
        messageDispatch({
          type: PUSH_MESSAGE,
          payload: {
            type: "info",
            header: "Token expired",
            body: "You have been logged out due to token expiration",
          },
        });
      }, authState.expireTime * 1000 - Date.now());
    } else {
      setRoutes(AppRoutes);
    }
    return () => clearTimeout(timer);
  }, [authState]);

  return (
    <div className="App">
      <AuthContext.Provider value={{ authState, authDispatch }}>
        <MessageContext.Provider value={{ messageState, messageDispatch }}>
          <BrowserRouter basename={baseUrl}>
            <div className="header">
              <NotifMessageQueue />
              <Navigation routes={routes} />
            </div>
            <div className="body">
              <Routes>
                {routes.map((route) => {
                  if (route.path !== "/")
                    return (
                      <Route
                        key={route.name.replace(/ /g, "-") + "-route"}
                        path={route.path}
                        element={route.page}
                      ></Route>
                    );
                  else
                    return (
                      <Route
                        index
                        key={route.name.replace(/ /g, "-") + "-route"}
                        path={route.path}
                        element={route.page}
                      ></Route>
                    );
                })}
                <Route path="*" element={<Navigate to="/" />} />
              </Routes>
            </div>
            <div className="footer">
              <div className="watermark">Made by Do Trung Anh</div>
            </div>
          </BrowserRouter>
        </MessageContext.Provider>
      </AuthContext.Provider>
    </div>
  );
}

export default App;
