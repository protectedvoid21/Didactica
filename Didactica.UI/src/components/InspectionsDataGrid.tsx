import { useMemo } from 'react';
import { useGetInspections } from '../hooks/useGetInspections';
import {
  MaterialReactTable,
  MRT_ColumnDef,
  useMaterialReactTable,
} from 'material-react-table';
import { InspectionDetails } from '../types';
import { Checkbox } from '@mui/material';
import { InspectionDetailsPanel } from './InspectionDetailsPanel';

export const InspectionsDataGrid = () => {
  const { data: inspections } = useGetInspections();

  const columns = useMemo<MRT_ColumnDef<InspectionDetails>[]>(() => [
    {
      accessorKey: 'course',
      header: 'Kurs',
      size: 250
    },
    {
      accessorKey: 'courseType',
      header: 'Rodzaj zajęć',
      size: 150
    },
    {
      accessorKey: 'date',
      header: 'Data zajęć',
      size: 100,
      Cell: ({ cell }) => new Date(cell.getValue()).toLocaleDateString(),
    },
    {
      accessorKey: 'isRemote',
      header: 'Czy zdalnie?',
      size: 35,
      Cell: ({ cell }) => (cell.getValue() ? <Checkbox checked disabled /> : <Checkbox disabled />),
    },
    {
      accessorKey: 'lessonEnvironment',
      header: 'Środowisko zajęć',
      size: 100
    },
    {
      accessorKey: 'place',
      header: 'Miejsce',
      size: 75
    }
  ],
    []
  );

  const table = useMaterialReactTable({
    columns,
    data: inspections?.data ?? [],
    enableExpanding: true,
    renderDetailPanel: (rowData) => (
      <div>
        <InspectionDetailsPanel inspectionDetails={rowData.row.original} />
      </div>
    )
  })

  return (
    <>
      <MaterialReactTable table={table} />
    </>
  )
}