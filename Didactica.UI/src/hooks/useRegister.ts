import { useMutation } from "@tanstack/react-query";
import urlJoin from "url-join";
import { RegisterRequest, ApiResponseData, AuthResult } from '../types';
import { api } from '../utils/api';
import { ACCOUNTS_URL } from '../utils/constants';


export const useRegister = () => useMutation({
  mutationFn: async (registerRequest: RegisterRequest) => (await api.post<ApiResponseData<AuthResult>>(urlJoin(ACCOUNTS_URL, 'register'), registerRequest)).data,
});