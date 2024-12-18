import { Outlet } from 'react-router';
import { NavBar } from './components/NavBar';

export const Layout = () => {
  return (
    <div>
      <NavBar />
      <Outlet />
    </div>
  );
}