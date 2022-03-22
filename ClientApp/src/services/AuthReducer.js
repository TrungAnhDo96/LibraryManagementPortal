export const LOGIN_SESSION = "LOGIN_SESSION";
export const LOGIN_LOCAL = "LOGIN_LOCAL";
export const LOGOUT_SESSION = "LOGOUT_SESSION";
export const LOGOUT = "LOGOUT";

export const authReducer = (state, action) => {
  switch (action.type) {
    case LOGIN_SESSION:
      sessionStorage.setItem("tokenKey", action.payload.tokenKey);
      return {
        ...state,
        isAuthenticated: true,
        userId: action.payload.userId,
        userName: action.payload.userName,
        tokenKey: action.payload.tokenKey,
        accessLevel: action.payload.accessLevel,
        expireTime: action.payload.expireTime,
      };
    case LOGIN_LOCAL:
      localStorage.setItem("tokenKey", action.payload.tokenKey);
      return {
        ...state,
        isAuthenticated: true,
        userId: action.payload.userId,
        userName: action.payload.userName,
        tokenKey: action.payload.tokenKey,
        accessLevel: action.payload.accessLevel,
        expireTime: action.payload.expireTime,
      };
    case LOGOUT_SESSION:
      if (!!sessionStorage.getItem("tokenKey"))
        sessionStorage.removeItem("tokenKey");

      return {
        ...state,
        isAuthenticated: false,
        userId: null,
        userName: null,
        tokenKey: null,
        accessLevel: null,
        expireTime: null,
      };
    case LOGOUT:
      if (!!sessionStorage.getItem("tokenKey"))
        sessionStorage.removeItem("tokenKey");

      if (!!localStorage.getItem("tokenKey"))
        localStorage.removeItem("tokenKey");

      return {
        ...state,
        isAuthenticated: false,
        userId: null,
        userName: null,
        tokenKey: null,
        accessLevel: null,
        expireTime: null,
      };
    default:
      break;
  }
};
