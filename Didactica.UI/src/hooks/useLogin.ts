import { useMutation } from "@tanstack/react-query";
import urlJoin from "url-join";
import { LoginRequest, ApiResponseData, AuthResult } from '../types';
import { api } from '../utils/api';
import { ACCOUNTS_URL } from '../utils/constants';

export const useLogin = () => useMutation({
  mutationFn: async (loginRequest: LoginRequest) => (await api.post<ApiResponseData<AuthResult>>(urlJoin(ACCOUNTS_URL, 'login'), loginRequest)).data,
})