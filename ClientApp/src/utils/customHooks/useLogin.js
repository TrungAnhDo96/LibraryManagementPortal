import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../App";
import { LOGIN_SESSION } from "../../services/AuthReducer";
import jwtDecode from "jwt-decode";

const useLogin = () => {
  const { authState, authDispatch } = React.useContext(AuthContext);
  const navigate = useNavigate();
  useEffect(() => {
    const token = sessionStorage.getItem("tokenKey");
    if (token !== null) {
      const decoded = jwtDecode(token);
      if (authState.isAuthenticated !== true) {
        authDispatch({
          type: LOGIN_SESSION,
          payload: {
            tokenKey: token,
            userId: decoded.UserId,
            userName: decoded.UserName,
            expireTime: decoded.exp,
            accessLevel: decoded.AccessLevel,
          },
        });
      }
    } else {
      navigate("/");
    }
    return () => {};
  }, []);
};

export default useLogin;
