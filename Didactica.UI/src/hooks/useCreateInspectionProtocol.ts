import { useMutation } from "@tanstack/react-query";
import urlJoin from "url-join";
import { ApiResponse } from '../types';
import { api } from '../utils/api';
import { INSPECTION_FORMS_URL } from '../utils/constants';

export const useCreateInspectionProtocol = (id: string) => useMutation({
  mutationFn: async (teacherIds: number[]) => (await api.post<ApiResponse>(urlJoin(INSPECTION_FORMS_URL, id), { "teacherIds": teacherIds })).data,
})