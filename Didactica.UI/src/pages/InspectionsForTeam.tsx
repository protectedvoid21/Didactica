import { InspectionsDataGrid } from '../components/InspectionsDataGrid';
import { useGetTeamInspections } from '../hooks/useGetTeamInspections';

export const InspectionsForTeam = () => {
  const { data } = useGetTeamInspections();

  return (
    <>
      <h1 className='text-3xl mb-4 font-bold'>Hospitacje z udziałem w komisji</h1>
      <InspectionsDataGrid inspections={data || []} includeTeacherName />
    </>
  );
}