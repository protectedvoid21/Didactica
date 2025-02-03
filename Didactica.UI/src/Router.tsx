import { BrowserRouter, createBrowserRouter, createRoutesFromElements, Route, Routes } from "react-router";
import { Layout } from './Layout';
import { Home } from './pages/Home';
import { AddInspectionProtocol } from './pages/AddInspection';
import { NonAuthRoute } from './utils/NonAuthRoute';
import Auth from './utils/Auth';
import { ProtectedRoute } from './utils/ProtectedRoute';
import { NotFound } from './pages/NotFound';
import { PlannedInspections } from './pages/PlannedInspections';
import { InspectionsForTeam } from './pages/InspectionsForTeam';
import { InspectionTeams } from './pages/InspectionTeams';

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
        <Route path='/dodawanie-protokolu' element={<AddInspectionProtocol />} />
        <Route path='/zaplanowane-hospitacje' element={<PlannedInspections />} />
        <Route path='/hospitacje-komisja' element={<InspectionsForTeam />} />
        <Route path='/zespoly-hospitacyjne' element={<InspectionTeams />} />
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