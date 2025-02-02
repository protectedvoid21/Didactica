export type ApiResponse = {
  message: string[];
  isSuccess: boolean;
}

export type ApiResponseData<T> = ApiResponse & {
  data: T;
}

export type LoginRequest = {
  userName: string;
  password: string;
}

export type AuthResult = {
  token: string;
  refreshToken: string;
}

export type RegisterRequest = {
  userName: string;
  password: string;
}

export type GetTeacherResponse = {
  id: number;
  name: string;
  lastName: string;
  faculty?: string;
  email?: string;
  phoneNumber?: string;
}

export type InspectionFormRequest = {
  inspectionId: number;
  wereClassesOnTime: boolean;
  wasAttendanceChecked: boolean;
  wasRoomSuitable: boolean;
  presentedTopicAndScope: number;
  explainedClearly: number;
  wasEngaged: number;
  encouragedIndependentThinking: number;
  maintainedDocumentation: number;
  deliveredUpdatedKnowledge: number;
  presentedPreparedMaterial: number;
  finalGradeJustification: string;
  conclusionsAndRecommendations: string;
  finalGrade: string;
}

export type InspectionDetails = {
  id: number;
  teacherId: number;
  teacherFirstName: string;
  teacherLastName: string;
  course: string;
  courseType: string;
  date: Date;
  isRemote: boolean;
  lessonEnvironment: string;
  place: string;
  getInspectionTeamResponse: {
    id: number;
    teachers: {
      item1: number;
      item2: string;
    }[];
  };
}

export type InspectionTeamTeacherResponse = {
  id: number;
  firstName: string;
  lastName: string;
}

export type GetInspectionTeamResponse = {
  id: number;
  createDate: Date;
  teachers: InspectionTeamTeacherResponse[];
}
