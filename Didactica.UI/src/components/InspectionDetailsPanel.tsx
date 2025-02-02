import { TextField } from '@mui/material';
import { InspectionDetails } from '../types';

export const InspectionDetailsPanel = ({ inspectionDetails }: { inspectionDetails: InspectionDetails }) => {
  const DataDisplay = ({ header, value }: { header: string, value: string | number }) => (
    <div>
      <h3 className='font-bold'>{header}</h3>
      <TextField value={value} disabled fullWidth />
    </div>
  )

  return (
    <>
      <div className='grid grid-cols-3 gap-4'>
        <DataDisplay header="Kurs" value={inspectionDetails.course} />
        <DataDisplay header="Rodzaj zajęć" value={inspectionDetails.courseType} />
        <DataDisplay header="Data zajęć" value={new Date(inspectionDetails.date).toLocaleString()} />
        <DataDisplay header="Zdalnie/Stacjonarnie" value={inspectionDetails.isRemote ? 'Zdalnie' : 'Stacjonarnie'} />
        <DataDisplay header="Środowisko zajęć" value={inspectionDetails.lessonEnvironment} />
        <DataDisplay header="Miejsce" value={inspectionDetails.place} />
      </div>
      <div className='py-4'>
        <h3 className='font-bold text-lg'>Skład komisji</h3>
        <ul>
          {inspectionDetails.getInspectionTeamResponse.teachers.map((teacher, index) => (
            <li key={index}>
              {teacher.item2}
            </li>
          ))}
        </ul>
      </div>
    </>
  )
}