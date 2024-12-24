import { Checkbox, FormControlLabel, FormGroup, TextField } from '@mui/material'

export const AddHospitation = () => {
  return (
    <div>
      <h1 className='text-4xl font-bold text-center mb-8'>Wprowadzanie wyników hospitacji</h1>
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
        <section className='flex-1 p-8'>
          <FormGroup className='flex flex-col gap-4'>
            <h3 className='text-xl font-semibold text-center'>Ocena formalna zajęć</h3>
            <FormControlLabel control={<Checkbox size='large' />} disableTypography
              label='Czy zajęcia rozpoczęły się punktualnie?' />
            <FormControlLabel control={<Checkbox size='large' />} disableTypography
              label='Czy sprawdzono obecność studentów?' />
            <FormControlLabel control={<Checkbox size='large' />} disableTypography
              label='Czy sala i jej wyposażenie są przystosowane do formy prowadzonych zajęć?' />
            <FormControlLabel control={<Checkbox size='large' />} disableTypography
              label={'Treść zajęć jest zgodna z programem kursu i umożliwia osiągnięcie założonych efektów ' +
                'uczenia się ujętych w Karcie Przedmiotu'} />
            <FormControlLabel control={<Checkbox size='large' />} disableTypography
              label='Zgoda na przetwarzanie danych osobowych' />
            <TextField
              rows={4}
              multiline
              value=' '
              label='Inne uwagi, wnioski i zalecenia dotyczące formalnej strony zajęć' />
          </FormGroup>
        </section>
      </div>
    </div>
  )
}