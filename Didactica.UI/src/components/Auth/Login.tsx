import { Button, TextField } from "@mui/material"
import { useNavigate, } from 'react-router';
import { useForm, SubmitHandler, Controller } from "react-hook-form";
import { enqueueSnackbar } from "notistack";
import { ErrorSnackbar } from '../ErrorSnackbar';
import { useLogin } from '../../hooks/useLogin';
import { useAuth } from '../../utils/AuthProvider';

interface IFormInput {
  userName: string;
  password: string;
}

export const Login = () => {
  const { handleSubmit, control, formState: { errors } } = useForm<IFormInput>();
  const auth = useAuth();

  const postLogin = useLogin();
  const navigate = useNavigate();

  const handleLogin: SubmitHandler<IFormInput> = async (data: IFormInput) => {
    const loginResult = await postLogin.mutateAsync(data);

    if (!loginResult.isSuccess) {
      loginResult.message.forEach((error) => {
        enqueueSnackbar(error, { variant: 'error' });
      });
      return;
    }

    auth.setToken(loginResult.data.token);
    navigate("/", { replace: true });
  }

  return (
    <>
      <h2 className='text-center mb-8 text-2xl'>Logowanie</h2>
      {postLogin.isError && <ErrorSnackbar error={postLogin.error.message} />}
      <form onSubmit={handleSubmit(handleLogin)} className='flex flex-col sm:gap-10 gap-4'>
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
        <Button type="submit" variant='contained'>Zaloguj</Button>
      </form>
    </>
  );
}