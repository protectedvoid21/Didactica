import { TextField } from '@mui/material';
import Checkbox from '@mui/material/Checkbox';
import { RadioScoreInput } from '../components/RadioScaleInput';
import { InspectionFormRequest } from '../types';

type FormControlLabelProps<T> = {
  label?: string;
  modelSelector: keyof T;
}

export const CustomCheckbox = (props: any) =>
  <Checkbox
    {...props}

  />;

export const classesFormFields: FormControlLabelProps<InspectionFormRequest>[] = [
  {
    label: 'Czy zajęcia rozpoczęły się punktualnie?',
    modelSelector: 'wereClassesOnTime'
  },
  {
    label: 'Czy sprawdzono obecność studentów?',
    modelSelector: 'wasAttendanceChecked'
  },
  {
    label: 'Czy sala i jej wyposażenie są przystosowane do formy prowadzonych zajęć?',
    modelSelector: 'wasRoomSuitable'
  }
];

export const teacherFormFields: FormControlLabelProps<InspectionFormRequest>[] = [
  {
    label: 'Przedstawił temat, cel i zakres zajęć',
    modelSelector: 'presentedTopicAndScope'
  },
  {
    label: 'Wyjaśniał w zrozumiały sposób omawiane zagadnienia',
    modelSelector: 'explainedClearly'
  },
  {
    label: 'Realizował zajęcia z zaangażowaniem',
    modelSelector: 'wasEngaged'
  },
  {
    label: 'Inspirował studentów do samodzielnego myślenia (stawiania pytań, dyskusji, samodzielnego rozwiązywania problemów/zadań itp.)',
    modelSelector: 'encouragedIndependentThinking'
  },
  {
    label: 'Prowadził dokumentację zajęć (lista obecności, lista ocen, sprawozdania, prace kontrolne itp.)',
    modelSelector: 'maintainedDocumentation'
  },
  {
    label: 'Przekazywał aktualną wiedzę',
    modelSelector: 'deliveredUpdatedKnowledge'
  },
  {
    label: 'Przedstawiał materiał, który był przygotowany i uporządkowany',
    modelSelector: 'presentedPreparedMaterial'
  },
];

export const summaryFormFields: FormControlLabelProps<InspectionFormRequest>[] = [
  {
    label: 'Uzasadnienie oceny końcowej',
    modelSelector: 'finalGradeJustification'
  },
  {
    label: 'Wnioski i zalecenia',
    modelSelector: 'conclusionsAndRecommendations'
  },
  {
    label: 'Ocena końcowa',
    modelSelector: 'finalGrade'
  }
];