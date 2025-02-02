import { BrowserRouter, createBrowserRouter, createRoutesFromElements, Route, Routes } from "react-router";
import { Layout } from './Layout';
import { Home } from './pages/Home';
import { AddInspection } from './pages/AddInspection';
import { NonAuthRoute } from './utils/NonAuthRoute';
import Auth from './utils/Auth';
import { ProtectedRoute } from './utils/ProtectedRoute';
import { NotFound } from './pages/NotFound';

const nonAuthRoutes = (
  <>
    <Route element={<NonAuthRoute />}>
      <Route path="/login" element={<Auth />} />
    </Route>
  </>
)

const authRoutes = (
  <>
    <Route element={<ProtectedRoute />}>
      <Route element={<Layout />}>
        <Route path='/' element={<Home />} />
        <Route path='*' element={<NotFound />} />
        <Route path='/dodawanie-hospitacji' element={<AddInspection />} />
      </Route>
    </Route>
  </>
);

export const Router = createBrowserRouter(
  createRoutesFromElements(
    <>
      {nonAuthRoutes}
      {authRoutes}
    </>
  ))