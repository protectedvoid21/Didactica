import { useQuery } from '@tanstack/react-query';
import { ApiResponseData, GetInspectionTeamResponse, InspectionDetails } from '../types';
import urlJoin from 'url-join';
import { api } from '../utils/api';
import { INSPECTION_TEAMS_URL, INSPECTIONS_URL } from '../utils/constants';

export const useGetInspectionTeams = () => useQuery({
  queryKey: ['inspection-teams'],
  queryFn: () => (
    api.get<ApiResponseData<GetInspectionTeamResponse[]>>(
      urlJoin(INSPECTION_TEAMS_URL)
    ).then(res => res.data.data))
})