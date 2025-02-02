import { useQuery } from '@tanstack/react-query';
import { api } from '../utils/api';
import { ApiResponseData, GetTeacherResponse } from '../types';
import urlJoin from 'url-join';
import { TEACHERS_URL } from '../utils/constants';

export const useGetTeachers = () => useQuery({
  queryKey: ['teachers'],
  queryFn: () => api.get<ApiResponseData<GetTeacherResponse[]>>(TEACHERS_URL).then(res => res.data.data)
})