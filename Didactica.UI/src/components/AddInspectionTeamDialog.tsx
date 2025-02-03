import { Dialog, DialogTitle, DialogContent, TextField, Button, Autocomplete, DialogActions } from '@mui/material'
import { DialogProps } from '../utils/dialogProps'
import { useState } from 'react';
import { useCreateInspectionTeam } from '../hooks/useCreateInspectionTeam';
import { useGetTeachers } from '../hooks/useGetTeachers';
import { useSnackbar } from 'notistack';
import { set } from 'react-hook-form';
import { GetTeacherResponse } from '../types';
import { Close, X } from '@mui/icons-material';

type Teacher = {
	id: number;
	firstName: string;
	lastName: string;
};


export const AddInspectionTeamDialog = ({ isOpen, onClose, onSuccessSubmit }: DialogProps) => {
	const [teachers, setTeachers] = useState<Teacher[]>([]);
	const [currentTeacher, setCurrentTeacher] = useState<GetTeacherResponse | null>(null);
	const { mutateAsync } = useCreateInspectionTeam();
	const { enqueueSnackbar } = useSnackbar();

	const { data: teacherList } = useGetTeachers();
	console.log(teachers);

	const handleAddTeacher = (teacher: GetTeacherResponse | null) => {
		if (!teacher) {
			enqueueSnackbar('Nie wybrano nauczyciela!', { variant: 'error' });
			return;
		}
		if (teachers.some(t => t.id === teacher.id)) {
			enqueueSnackbar('Nauczyciel już jest w komisji', { variant: 'error' });
			return;
		}
		setTeachers([...teachers, { id: teacher.id, firstName: teacher.name, lastName: teacher.lastName }]);
		setCurrentTeacher(null);
	};

	const removeTeacher = (teacher: Teacher) => {
		setTeachers(teachers.filter(t => t.id !== teacher.id));
	}

	const handleSubmit = async () => {
		if (teachers.length === 0) {
			enqueueSnackbar('Komisja musi składać się z przynajmniej jednego nauczyciela', { variant: 'error' });
			return;
		}
		const addResult = await mutateAsync(teachers.map(t => t.id));
		if (addResult.isSuccess) {
			onSuccessSubmit();
		}
	};

	return (
		<>
			<Dialog open={isOpen} onClose={onClose} fullWidth maxWidth='md'>
				<DialogTitle>Dodaj nową komisję</DialogTitle>
				<DialogContent>
					<div className='my-4'>
						<h3>Nauczyciele w komisji</h3>
						<ul>
							{teachers.map((teacher, index) => (
								<li
									key={index}
									className='flex justify-between items-baseline p-4 m-2 rounded-xl bg-slate-100'
								>
									<div>{teacher.firstName} {teacher.lastName}</div>
									<div><Button variant='contained' color='info' onClick={() => removeTeacher(teacher)}><Close /></Button></div>
								</li>
							))}
						</ul>
					</div>
					<Autocomplete
						options={teacherList || []}
						value={currentTeacher}
						onChange={(_, newValue) => setCurrentTeacher(newValue)}
						getOptionLabel={(option) => option.name + ' ' + option.lastName}
						renderInput={(params) => <TextField {...params} label='Nauczyciel' />}
					/>
				</DialogContent>
				<DialogActions>
					<Button onClick={() => handleAddTeacher(currentTeacher)} variant='contained'>Dodaj nauczyciela</Button>
					<Button onClick={handleSubmit} variant='contained'>Utwórz komisję</Button>
				</DialogActions>
			</Dialog>
		</>
	)
}