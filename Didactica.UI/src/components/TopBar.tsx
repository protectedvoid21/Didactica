import { Avatar } from '@mui/material';

export const TopBar = () => {
  return (
    <>
      <header className='flex justify-between'>
        <div className='text-4xl'>{new Date().toDateString()}</div>
        <div>
          <Avatar alt='Remy Sharp' src='https://mui.com/static/images/avatar/1.jpg' />
        </div>
      </header>
    </>
  );
}