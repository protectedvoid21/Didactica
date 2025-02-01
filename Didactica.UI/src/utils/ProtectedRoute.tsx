import { Navigate, Outlet } from "react-router";
import { useAuth } from "./AuthProvider";

export const ProtectedRoute = () => {
  const auth = useAuth();

  if (!auth?.token) {
    return <Navigate to="/login" />;
  }

  return <Outlet />;
};