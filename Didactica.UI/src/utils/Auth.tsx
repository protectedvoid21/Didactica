import { Paper, Tab, Typography } from "@mui/material"
import { TabContext, TabList, TabPanel } from "@mui/lab";
import { useState } from "react";
import { Login } from '../components/Auth/Login';
import { Register } from '../components/Auth/Register';

export const Auth = () => {
  const [value, setValue] = useState('1');

  const handleChange = (event: React.SyntheticEvent, newValue: string) => {
    setValue(newValue);
  }

  return (
    <>
      <div className='flex flex-col m-auto sm:w-1/2 max-w-[1000px] h-svh justify-center'>
        <div className="mb-8">
          <Typography variant='h3' className='text-center'>Didactica</Typography>
          <Typography variant='h5' className='text-center'>Zaloguj się lub zarejestruj</Typography>
        </div>
        <Paper elevation={3} className='flex flex-col justify-between gap-10 p-4 sm:p-20'>
          <TabContext value={value}>
            <TabList onChange={handleChange} sx={{ borderBottom: 1, borderColor: 'divider', display: 'flex', justifyContent: 'space-between' }}>
              <Tab label='Logowanie' value='1' />
              <Tab label='Rejestracja' value='2' />
            </TabList>
            <TabPanel value='1'>
              <Login />
            </TabPanel>
            <TabPanel value='2'>
              <Register />
            </TabPanel>
          </TabContext>
        </Paper>
      </div>
    </>
  );
}

export default Auth;