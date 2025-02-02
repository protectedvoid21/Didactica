import { InspectionsDataGrid } from '../components/InspectionsDataGrid';
import { useGetPlannedInspections } from '../hooks/useGetPlannedInspections';

export const PlannedInspections = () => {
  const { data } = useGetPlannedInspections();

  return (
    <>
      <h1 className='text-3xl mb-4 font-bold'>Planowane hospitacje</h1>
      <InspectionsDataGrid includeTeacherName inspections={data || []} />
    </>
  );
}