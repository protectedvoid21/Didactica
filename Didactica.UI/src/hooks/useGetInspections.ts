import { useQuery } from '@tanstack/react-query';
import { ApiResponseData, InspectionDetails } from '../types';
import urlJoin from 'url-join';
import { api } from '../utils/api';
import { INSPECTIONS_URL } from '../utils/constants';

export const useGetInspections = (teacherId?: string | number) => useQuery({
  queryKey: ['inspections'],
  queryFn: async () => (
    await api.get<ApiResponseData<InspectionDetails[]>>(urlJoin(INSPECTIONS_URL + (teacherId ? `teachers/${teacherId}` : '')))
  ).data,
})