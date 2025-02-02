import { TableContainer } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'
import { InspectionsDataGrid } from '../components/InspectionsDataGrid'

export const Home = () => {
  return (
    <>
      <h1 className='text-3xl mb-4 font-bold'>Hospitacje</h1>
      <InspectionsDataGrid />
    </>
  )
}