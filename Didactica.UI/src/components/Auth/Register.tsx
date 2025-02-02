import { Button, TextField } from "@mui/material"
import { useNavigate } from 'react-router';
import { useForm, SubmitHandler, Controller } from "react-hook-form";
import { useSnackbar } from 'notistack';
import { useRegister } from '../../hooks/useRegister';

interface IFormInput {
  userName: string;
  email: string,
  password: string;
}

export const Register = () => {
  const { handleSubmit, control, formState: { errors } } = useForm<IFormInput>();
  const { enqueueSnackbar } = useSnackbar();
  const navigate = useNavigate();

  const postRegister = useRegister();

  const handleLogin: SubmitHandler<IFormInput> = async (data: IFormInput) => {
    const registerResult = await postRegister.mutateAsync(data);

    if (!registerResult.isSuccess) {
      registerResult.message.forEach((error) => {
        enqueueSnackbar(error, { variant: 'error' });
      });
      return;
    }

    enqueueSnackbar('Zostałeś zarejestrowany! Zaloguj się przy pomocy wcześniej podanych danych', { variant: 'success' });
    navigate('/login');
  }

  return (
    <>
      <h2 className='text-center mb-8 text-2xl'>Rejestracja</h2>
      <form onSubmit={handleSubmit(handleLogin)} className='flex flex-col gap-10'>
        <Controller
          name="userName"
          control={control}
          defaultValue=""
          rules={{ required: 'Nazwa użytkownika jest wymagana' }}
          render={({ field }) => (
            <TextField
              {...field}
              label="Nazwa użytkownika"
              variant="outlined"
              inputProps={{
                type: 'text',
                autoComplete: 'off'
              }}
              error={!!errors.userName}
              helperText={errors.userName ? errors.userName.message : ''}
            />
          )}
        />
        <Controller
          name="email"
          control={control}
          defaultValue=""
          rules={{ required: 'Email jest wymagany' }}
          render={({ field }) => (
            <TextField
              {...field}
              label="Email"
              variant="outlined"
              type='password'
              inputProps={{
                type: 'text',
                autoComplete: 'off'
              }}
              error={!!errors.email}
              helperText={errors.email ? errors.email.message : ''}
            />
          )}
        />
        <Controller
          name="password"
          control={control}
          defaultValue=""
          rules={{ required: 'Hasło jest wymagane' }}
          render={({ field }) => (
            <TextField
              {...field}
              label="Hasło"
              variant="outlined"
              type='password'
              inputProps={{
                type: 'password',
                autoComplete: 'new-password'
              }}
              error={!!errors.password}
              helperText={errors.password ? errors.password.message : ''}
            />
          )}
        />
        <Button type="submit" variant='contained'>Zarejestruj</Button>
      </form>
    </>
  );
}