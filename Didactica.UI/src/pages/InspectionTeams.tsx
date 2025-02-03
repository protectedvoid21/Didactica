import { useMemo, useState } from 'react'
import { useGetInspectionTeams } from '../hooks/useGetInspectionTeams'
import { MaterialReactTable, MRT_ColumnDef } from 'material-react-table'
import { GetInspectionTeamResponse, InspectionTeamTeacherResponse } from '../types'
import { AddInspectionTeamDialog } from '../components/AddInspectionTeamDialog'
import { useSnackbar } from 'notistack'
import { Button } from '@mui/material'
import { Add } from '@mui/icons-material'

export const InspectionTeams = () => {
	const { data } = useGetInspectionTeams()
	const [isAddDialogOpen, setIsAddDialogOpen] = useState(false)
	const { enqueueSnackbar } = useSnackbar()

	const onAddSuccess = () => {
		setIsAddDialogOpen(false)
		enqueueSnackbar('Zespół hospitacyjny został dodany', { variant: 'success' })
	}

	const columns = useMemo<MRT_ColumnDef<GetInspectionTeamResponse>[]>(() => [
		{
			accessorKey: 'createDate',
			header: 'Create Date',
			Cell: ({ cell }) => new Date(cell.getValue<string>()).toLocaleString(),
		},
		{
			accessorKey: 'teachers',
			header: 'Teachers',
			Cell: ({ cell }) => {
				const teachers = cell.getValue<InspectionTeamTeacherResponse[]>()
				return teachers.map(teacher => `${teacher.firstName} ${teacher.lastName}`).join(', ')
			},
		}
	], [])

	return (
		<>
			<div className='flex gap-4 items-baseline'>
				<h1 className='text-3xl mb-4 font-bold'>Zespoły hospitacyjne</h1>
				<Button startIcon={<Add />}
					variant='contained'
					onClick={() => setIsAddDialogOpen(true)}
					sx={{ borderRadius: '3rem' }}>
					Dodaj zespół hospitacyjny
				</Button>
			</div>
			<MaterialReactTable
				columns={columns}
				initialState={{ sorting: [{ id: 'createDate', desc: true }] }}
				data={data || []} />
			<AddInspectionTeamDialog
				isOpen={isAddDialogOpen}
				onClose={() => setIsAddDialogOpen(false)}
				onSuccessSubmit={onAddSuccess}
			/>
		</>
	)
}