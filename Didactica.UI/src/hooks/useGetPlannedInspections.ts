import { useQuery } from '@tanstack/react-query';
import { api } from '../utils/api';
import { ApiResponseData, InspectionDetails } from '../types';
import urlJoin from 'url-join';
import { INSPECTION_TEAMS_URL, INSPECTIONS_URL } from '../utils/constants';

export const useGetPlannedInspections = () => useQuery({
  queryKey: ['inspections-for-team'],
  queryFn: () => api.get<ApiResponseData<InspectionDetails[]>>(urlJoin(INSPECTIONS_URL, 'planned')).then(res => res.data.data)
})