import React, { useEffect, useState } from "react";
import { Button, Form, Spinner } from "react-bootstrap";
import { AuthContext, MessageContext } from "../App";
import { LOGIN_SESSION } from "../services/AuthReducer";
import { PUSH_MESSAGE } from "../services/MessageReducer";
import axios from "axios";
import "./Login.css";
import jwtDecode from "jwt-decode";

function Login() {
  const { messageDispatch } = React.useContext(MessageContext);
  const { authDispatch } = React.useContext(AuthContext);

  const initialLoginDetails = {
    userName: null,
    password: null,
    isRememberMe: false,
  };

  const [loginDetails, setLoginDetails] = useState(initialLoginDetails);
  const [isLoading, setIsLoading] = useState(false);

  function handleOnSubmit(e) {
    e.preventDefault();
    if (validate() === true) {
      setIsLoading(true);
    }
  }

  function handleLoginInput(e) {
    setLoginDetails({ ...loginDetails, [e.target.id]: e.target.value });
  }

  function handleRememberMe(e) {
    setLoginDetails({
      ...loginDetails,
      isRememberMe: !loginDetails.isRememberMe,
    });
  }

  function validate() {
    var result = true;
    return result;
  }

  useEffect(() => {
    if (isLoading === true) {
      //fetch refresh token
      axios({
        method: "post",
        url: "https://localhost:7182/api/auth/login",
        headers: { "Content-Type": "application/json" },
        data: loginDetails,
      })
        .then((res) => {
          const decoded = jwtDecode(res.data);
          authDispatch({
            type: LOGIN_SESSION,
            payload: {
              tokenKey: res.data,
              userId: decoded.UserId,
              userName: decoded.UserName,
              expireTime: decoded.exp,
              accessLevel: decoded.AccessLevel,
            },
          });
          messageDispatch({
            type: PUSH_MESSAGE,
            payload: {
              type: "success",
              header: "Success",
              body: "Logged In successfully",
            },
          });
        })
        .catch((e) => {
          messageDispatch({
            type: PUSH_MESSAGE,
            payload: {
              type: "danger",
              header: "Failed",
              body: "Logged In failed.\n" + e,
            },
          });
        })
        .finally(() => {
          setIsLoading(false);
        });
    }
    return () => {};
  }, [isLoading, authDispatch, loginDetails, messageDispatch]);

  return (
    <div className="Login">
      {isLoading !== true ? (
        <div className="loginForm">
          <Form onSubmit={handleOnSubmit}>
            <Form.Group
              className="formGroup"
              controlId="userName"
              onChange={handleLoginInput}
            >
              <Form.Label>User name</Form.Label>
              <Form.Control type="text" placeholder="Enter user name" />
            </Form.Group>
            <Form.Group
              className="formGroup"
              controlId="password"
              onChange={handleLoginInput}
            >
              <Form.Label>Password</Form.Label>
              <Form.Control type="password" placeholder="Enter password" />
            </Form.Group>
            <Form.Group className="formGroup" controlId="isRememberMe">
              <Form.Check
                label="Remember me"
                value={loginDetails.isRememberMe}
                onChange={handleRememberMe}
              />
            </Form.Group>
            <Form.Group className="formGroup">
              <Button type="submit" variant="primary">
                Sign in
              </Button>
            </Form.Group>
          </Form>
        </div>
      ) : (
        <Spinner animation="border" role="status">
          <span className="visually-hidden">Loading...</span>
        </Spinner>
      )}
    </div>
  );
}

export default Login;
