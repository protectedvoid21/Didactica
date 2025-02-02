import { createContext, ReactElement, useContext, useEffect, useMemo, useState } from "react";
import { api } from "./api";
import { jwtDecode, JwtPayload } from "jwt-decode";

interface AuthContextType {
  token: string | null;
  userName: string | undefined;
  roles: | string | undefined;
  isAdmin: boolean | undefined;
  setToken: (newToken: string | null) => void;
}

const AuthContext = createContext({} as AuthContextType);

export const AuthProvider = ({ children }: { children: ReactElement }) => {
  const [token, setToken_] = useState(localStorage.getItem("token"));

  const setToken = (newToken: string | null) => {
    setToken_(newToken);
  }

  const decodedToken: JwtPayload | undefined = token != null ? jwtDecode(token) : undefined;
  const userName = decodedToken?.sub;
  const isAdmin = userName === "Admin";
  const roles = decodedToken?.["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

  api.defaults.headers.common["Authorization"] = "Bearer " + token;

  const contextValue = useMemo(
    () => ({
      token,
      userName: decodedToken?.sub,
      roles,
      isAdmin,
      setToken,
    }),
    [token, isAdmin, userName]
  );

  useEffect(() => {
    if (token) {
      api.defaults.headers.common["Authorization"] = "Bearer " + token;
      localStorage.setItem('token', token);
    }
    else {
      delete api.defaults.headers.common["Authorization"];
      localStorage.removeItem('token')
    }
  }, [token]);

  return (
    <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>
  );
};

export const useAuth = () => {
  return useContext(AuthContext);
}

export default AuthProvider;