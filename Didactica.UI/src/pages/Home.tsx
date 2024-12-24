import { TableContainer } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'

export const Home = () => {
  const columns = [
    { field: 'date', headerName: 'Data', flex: 0.6 },
    { field: 'subjectName', headerName: 'Przedmiot', flex: 2 },
    { field: 'grade', headerName: 'Ocena', flex: 1 },
    { field: 'status', headerName: 'Status', flex: 1 },
  ];

  const rows = [
    { id: 1, subjectName: 'Analiza matematyczna I', grade: null, date: '2024-10-01', status: 'W trakcie oceny' },
    { id: 2, subjectName: 'Analiza matematyczna II', grade: 4, date: '2023-04-15', status: 'Oceniona' },
    { id: 3, subjectName: 'Algebra z geometrią analityczną', grade: 3, date: '2021-01-19', status: 'Oceniona' },
    { id: 4, subjectName: 'Analiza matematyczna I', grade: 5.5, date: '2021-09-01', status: 'Oceniona' },
  ];

  return (
    <>
      <h1 className='text-3xl mb-4 font-bold'>Hospitacje</h1>
      <DataGrid columns={columns} rows={rows} />
    </>
  )
}