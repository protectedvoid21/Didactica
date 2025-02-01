import axios from 'axios';
import { API_URL } from './constants';

export const api = axios.create({
  baseURL: API_URL,
});

api.interceptors.response.use((response: any) => {
  return response;
}, (error: any) => {
  if (error.response && error.response.status < 500) {
    return error.response;
  }

  return Promise.reject(error);
})