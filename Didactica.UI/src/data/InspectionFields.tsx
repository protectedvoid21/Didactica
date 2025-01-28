import { TextField } from '@mui/material';
import Checkbox from '@mui/material/Checkbox';
import { RadioScoreInput } from '../components/RadioScaleInput';

type FormControlLabelProps = {
  label?: string;
  control: JSX.Element;
  labelPlacement?: 'end' | 'start' | 'top' | 'bottom';
}

export const CustomCheckbox = () => <Checkbox sx={{ '& .MuiSvgIcon-root': { fontSize: '1.rem' } }} />;

export const classesFormFields: FormControlLabelProps[] = [
  {
    label: 'Czy zajęcia rozpoczęły się punktualnie?',
    control: <CustomCheckbox />
  },
  {
    label: 'Czy sprawdzono obecność studentów?',
    control: <CustomCheckbox />
  },
  {
    label: 'Czy sala i jej wyposażenie są przystosowane do formy prowadzonych zajęć?',
    control: <CustomCheckbox />
  }
];

export const teacherFormFields: FormControlLabelProps[] = [
  {
    label: 'Przedstawił temat, cel i zakres zajęć',
    control: <RadioScoreInput onValueChange={() => { }} />,
    labelPlacement: 'top'
  },
  {
    label: 'Wyjaśniał w zrozumiały sposób omawiane zagadnienia',
    control: <RadioScoreInput onValueChange={() => { }} />,
    labelPlacement: 'top'
  },
  {
    label: 'Realizował zajęcia z zaangażowaniem',
    control: <RadioScoreInput onValueChange={() => { }} />,
    labelPlacement: 'top'
  },
  {
    label: 'Inspirował studentów do samodzielnego myślenia (stawiania pytań, dyskusji, samodzielnego rozwiązywania problemów/zadań itp.)',
    control: <RadioScoreInput onValueChange={() => { }} />,
    labelPlacement: 'top'
  },
  {
    label: 'Prowadził dokumentację zajęć (lista obecności, lista ocen, sprawozdania, prace kontrolne itp.)',
    control: <RadioScoreInput onValueChange={() => { }} />,
    labelPlacement: 'top'
  },
  {
    label: 'Przekazywał aktualną wiedzę',
    control: <RadioScoreInput onValueChange={() => { }} />,
    labelPlacement: 'top'
  },
  {
    label: 'Przedstawiał materiał, który był przygotowany i uporządkowany',
    control: <RadioScoreInput onValueChange={() => { }} />,
    labelPlacement: 'top'
  },
];

export const summaryFormFields: FormControlLabelProps[] = [
  {
    control: <TextField
      rows={4}
      multiline
      value=' '
      label={'Uzasadnienie oceny końcowej'}
    />
  },
  {
    control: <TextField
      rows={4}
      multiline
      value=' '
      label={'Wnioski i zalecenia'}
    />
  },
  {
    control: <TextField
      rows={4}
      multiline
      value=' '
      label={'Ocena końcowa'}
    />
  }
];