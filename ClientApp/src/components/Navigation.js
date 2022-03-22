import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { AuthContext, MessageContext } from "../App";
import { LOGOUT } from "../services/AuthReducer";
import { PUSH_MESSAGE } from "../services/MessageReducer";
import "./Navigation.css";

function Navigation(props) {
  const { authState, authDispatch } = React.useContext(AuthContext);
  const { messageDispatch } = useContext(MessageContext);

  function handleLogout() {
    authDispatch({ type: LOGOUT });
    messageDispatch({
      type: PUSH_MESSAGE,
      payload: {
        type: "success",
        header: "Success",
        body: "Logged out successfully",
      },
    });
  }

  return (
    <div className="Navigation">
      <nav>
        <ul>
          {props.routes.map((route) => {
            return (
              <li
                className="mainRoute"
                key={route.name.replace(/ /g, "-") + "-link"}
              >
                <Link to={"" + route.path}>{route.name}</Link>
              </li>
            );
          })}
          {authState.isAuthenticated === true ? (
            <li className="logoutRoute">
              <div className="loginDetails">
                Welcome, {authState.userName}.{" "}
                <Link to="/login" onClick={handleLogout}>
                  Logout
                </Link>
              </div>
            </li>
          ) : null}
        </ul>
      </nav>
    </div>
  );
}

export default Navigation;
