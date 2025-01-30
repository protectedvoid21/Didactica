import { FormControlLabel, Radio, RadioGroup } from '@mui/material';

type Props = {
  field: {
    value: string | number;
    onChange: (value: any) => void;
  };
  labels: string[];
};

export const RadioScaleInput = ({ field, labels }: Props) => {
  return (
    <RadioGroup
      row
      {...field}
      value={field.value}
      onChange={field.onChange}
      className="my-4"
    >
      {labels.map((label, index) => (
        <FormControlLabel
          key={label + '-label-' + index}
          value={index.toString()}
          control={
            <Radio
              key={label + '-radio-' + index}
              value={index}
              sx={{ padding: '0.25rem' }}
            />
          }
          label={label}
          labelPlacement="bottom"
        />
      ))}
    </RadioGroup>
  );
};
export const RadioScoreInput = (field: { value: string | number, onChange: (value: any) => void }) => {
  const labels = ['5.5', '5', '4', '3', '2', '0'];
  return <RadioScaleInput labels={labels} field={field} />;
}