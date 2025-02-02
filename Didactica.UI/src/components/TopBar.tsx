import { Avatar, Button } from '@mui/material';
import { useAuth } from '../utils/AuthProvider';
import { useNavigate } from 'react-router';
import { useSnackbar } from 'notistack';
import { roleTranslations } from '../utils/utils';

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
          <div className=''>
            {roleTranslations[auth.roles] ?? 'Nauczyciel'}
          </div>
          <div>
            {auth.userName}
          </div>
          <Button
            onClick={() => {
              navigator.clipboard.writeText(auth.token);
              enqueueSnackbar('Token skopiowany do schowka', { variant: 'success' });
            }}
            variant='outlined'
          >
            Kopiuj token
          </Button>
          <Button onClick={logout} variant='contained'>
            Wyloguj
          </Button>
        </div>
      </header>
    </>
  );
}