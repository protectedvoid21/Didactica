import { Button, Checkbox, FormControlLabel, FormGroup, TextField } from '@mui/material'
import SendIcon from '@mui/icons-material/Send';
import { classesFormFields, teacherFormFields, summaryFormFields } from '../data/InspectionFields';
import { Controller, useForm } from 'react-hook-form';
import { RadioScoreInput } from '../components/RadioScaleInput';
import { useSnackbar } from 'notistack';
import { InspectionFormRequest } from '../types';

export const AddInspection = () => {
  const { control, handleSubmit, formState: { errors } } = useForm<InspectionFormRequest>();
  const { enqueueSnackbar } = useSnackbar();

  const onSubmit = handleSubmit((data: InspectionFormRequest) => {
    enqueueSnackbar('Protokół został utworzony!', {
      variant: 'success'
    });
  });

  console.log(errors);

  return (
    <form onSubmit={onSubmit} className='flex flex-col gap-8 pb-4'>
      <h1 className='text-4xl font-bold text-center mb-8'>Wprowadzanie wyników hospitacji</h1>
      <div className='overflow-y-scroll scrollbar' style={{ height: 'calc(100vh - 16rem)' }}>
        <div className='flex flex-row gap-4'>
          <section className='flex flex-col flex-1 bg-primary-200 rounded-xl gap-8 p-8'>
            <h2 className='text-2xl font-semibold text-center'>Dane przeprowadzonej hospitacji</h2>
            <TextField label='Data przeprowadzenia' value='2024-10-03' disabled />
            <TextField label='Imię i nazwisko prowadzącego' value='Szymon Szcześniak' disabled />
            <TextField label='Kurs' value='Informatyka w biznesie' disabled />
            <TextField label='Rodzaj zajęć' value='Wykład' disabled />
            <TextField label='Forma przeprowadzenia zajęć' value='Stacjonarna' disabled />
            <TextField label='Miejsce zajęć' value='Bud. C-13 s.128' disabled />
            <TextField label='Środowisko realizacji zajęć' value='Nie dotyczy' disabled />
          </section>
          <section className='flex-1 p-8 overflow-y-auto'>
            <FormGroup className='flex flex-col gap-4'>
              <h3 className='text-xl font-semibold text-center'>Ocena formalna zajęć</h3>
              {classesFormFields.map((field, index) =>
                <Controller
                  key={'classes-form-field-' + index}
                  name={field.modelSelector}
                  control={control}
                  render={({ field: controllerField }) =>
                    <FormControlLabel
                      key={'classes-form-field-label-' + index}
                      control={
                        <Checkbox {...controllerField}
                          checked={controllerField.value}
                          onChange={(e) => controllerField.onChange(e.target.checked)}
                          sx={{ '& .MuiSvgIcon-root': { fontSize: '1.rem' } }}
                        />
                      }
                      disableTypography
                      labelPlacement={'end'}
                      label={field.label}
                    />
                  }
                />
              )}
            </FormGroup>
            <FormGroup className='flex flex-col gap-4 mt-8 text-start'>
              <h3 className='text-xl font-semibold text-center'>Prowadzący</h3>
              {teacherFormFields.map((field, index) =>
                <Controller
                  key={'teacher-form-field-' + index}
                  name={field.modelSelector}
                  control={control}
                  rules={{ required: 'Wybierz ocenę!' }}
                  render={({ field: controllerField, fieldState }) =>
                    <div className='flex flex-col gap-2'>
                      <FormControlLabel
                        labelPlacement='top'
                        label={field.label}
                        control={
                          <div>
                            {fieldState.error && (
                              <span className='text-red-500 font-normal text-sm'>{fieldState.error.message}</span>
                            )}
                            <RadioScoreInput
                              value={controllerField.value}
                              onChange={controllerField.onChange}
                            />
                          </div>
                        }
                      />
                    </div>
                  }
                />
              )}
              {summaryFormFields.map((field) => (
                <Controller
                  key={'summary-form-field-' + field.modelSelector}
                  name={field.modelSelector}
                  control={control}
                  rules={{ required: 'Należy podać odpowiedź' }}
                  render={({ field: controllerField, fieldState }) =>
                    <TextField
                      {...controllerField}
                      label={field.label}
                      rows={4}
                      multiline
                      error={!!fieldState.error}
                      helperText={fieldState.error?.message || ' '}
                    />
                  }
                />
              ))}

              <Button
                type='submit'
                variant='contained'
                color='primary'
                style={{ borderRadius: '2rem', paddingInline: '1.5rem', paddingBlock: '0.75rem' }}
                startIcon={<SendIcon />}
              >
                Zapisz
              </Button>
            </FormGroup>
          </section>
        </div>
      </div >
    </form >
  )
}