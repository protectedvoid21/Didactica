import { Avatar, Button } from '@mui/material';
import { useAuth } from '../utils/AuthProvider';
import { useNavigate } from 'react-router';
import { useSnackbar } from 'notistack';

export const TopBar = () => {
  const auth = useAuth();
  const navigate = useNavigate();
  const { enqueueSnackbar } = useSnackbar();

  const logout = () => {
    auth.setToken(null);
    enqueueSnackbar('Zostałeś wylogowany', { variant: 'success' });
    navigate('/login');
  }

  return (
    <>
      <header className='flex justify-between'>
        <div className='text-4xl'>{new Date().toDateString()}</div>
        <div className='flex gap-4 items-baseline'>
          {auth.userName}
          <Button onClick={logout} variant='contained'>
            Wyloguj
          </Button>
        </div>
      </header>
    </>
  );
}