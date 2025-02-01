import { Navigate, Outlet } from "react-router";
import { useAuth } from "./AuthProvider";

export const NonAuthRoute = () => {
  const auth = useAuth();

  if (auth?.token) {
    return <Navigate to="/" />;
  }

  return <Outlet />;
}