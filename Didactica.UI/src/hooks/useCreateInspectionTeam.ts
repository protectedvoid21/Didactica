import { useMutation } from "@tanstack/react-query";
import urlJoin from "url-join";
import { ApiResponse } from '../types';
import { api } from '../utils/api';
import { INSPECTION_TEAMS_URL } from '../utils/constants';

export const useCreateInspectionTeam = () => useMutation({
  mutationFn: async (teacherIds: number[]) => (await api.post<ApiResponse>(urlJoin(INSPECTION_TEAMS_URL), { "teacherIds": teacherIds })).data,
})