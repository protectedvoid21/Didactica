import { FormControlLabel, Radio, RadioGroup } from '@mui/material';

interface Props {
  labels: string[];
  onValueChange: (value: number) => void;
}

export const RadioScaleInput = ({ labels, onValueChange }: Props) => {
  return (
    <RadioGroup row className='my-4'>
      {labels.map((label, index) =>
        <FormControlLabel
          control={
            <Radio
              key={index}
              value={index}
              sx={{ padding: '0.25rem' }}
              onChange={() => onValueChange(index)}
            />}
          label={label}
          labelPlacement='bottom'
        />
      )}
    </RadioGroup>
  )
}

export const RadioScoreInput = ({ onValueChange }: { onValueChange: (value: number) => void }) => {
  const labels = ['5.5', '5', '4', '3', '2', '0'];
  return <RadioScaleInput labels={labels} onValueChange={onValueChange} />;
}