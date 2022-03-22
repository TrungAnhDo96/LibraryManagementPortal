import React from "react";
import { Button } from "react-bootstrap";
import { AuthContext, MessageContext } from "../App";
import { LOGOUT } from "../services/AuthReducer";
import { PUSH_MESSAGE } from "../services/MessageReducer";
import useLogin from "../utils/customHooks/useLogin";

function AppHomePage() {
  const { authState, authDispatch } = React.useContext(AuthContext);
  const { messageDispatch } = React.useContext(MessageContext);

  function handleLogout() {
    authDispatch({ type: LOGOUT });
    messageDispatch({
      type: PUSH_MESSAGE,
      payload: {
        type: "success",
        header: "Success",
        body: "Log out successful",
      },
    });
  }

  useLogin();

  return (
    <div className="HomePage">
      <h1>LMP Home Page</h1>
      <p>A Portal to manage book borrowing requests</p>
      {authState.isAuthenticated === true ? (
        <div className="loginStatus">
          <p>You have logged in as {authState.userName}.</p>
          <Button type="button" variant="danger" onClick={handleLogout}>
            Logout
          </Button>
        </div>
      ) : (
        <div className="loginStatus">
          <p>You have not logged in</p>
        </div>
      )}
    </div>
  );
}

export default AppHomePage;
