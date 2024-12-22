import { Outlet } from 'react-router';
import { NavBar } from './components/NavBar';
import { TopBar } from './components/TopBar';

export const Layout = () => {
  return (
    <div className='flex flex-row h-screen p-12 text-primary-800'>
      <div>
        <NavBar />
      </div>
      <div className='w-3/5 mx-auto'>
        <TopBar />
        <Outlet />
      </div>
    </div>
  );
}