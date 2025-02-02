import { useQuery } from '@tanstack/react-query';
import { ApiResponseData, InspectionDetails } from '../types';
import { api } from '../utils/api';
import { INSPECTION_TEAMS_URL } from '../utils/constants';
import urlJoin from 'url-join';

export const useGetTeamInspections = () => useQuery({
  queryKey: ['inspections-for-team'],
  queryFn: () => api.get<ApiResponseData<InspectionDetails[]>>(urlJoin(INSPECTION_TEAMS_URL, 'inspections')).then(res => res.data.data)
})