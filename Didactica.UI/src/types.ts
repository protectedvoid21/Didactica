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