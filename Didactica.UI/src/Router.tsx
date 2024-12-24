import { BrowserRouter, Route, Routes } from "react-router";
import { Layout } from './Layout';
import { Home } from './pages/Home';
import { AddHospitation } from './pages/AddHospitation';

export const Router = () => (
  <BrowserRouter>
    <Routes>
      <Route element={<Layout />}>
        <Route path='/' element={<Home />} />
        <Route path='/dodawanie-hospitacji' element={<AddHospitation />} />
      </Route>
    </Routes>
  </BrowserRouter>
);
