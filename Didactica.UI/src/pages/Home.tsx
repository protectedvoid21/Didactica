import { TableContainer } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'
import { InspectionsDataGrid } from '../components/InspectionsDataGrid'
import { useGetInspections } from '../hooks/useGetInspections'

export const Home = () => {
  const { data } = useGetInspections()

  return (
    <>
      <h1 className='text-3xl mb-4 font-bold'>Hospitacje</h1>
      <InspectionsDataGrid inspections={data?.data || []} />
    </>
  )
}