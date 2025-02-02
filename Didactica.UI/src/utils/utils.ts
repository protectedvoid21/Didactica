import { ReactNode } from 'react';

export function getPageCount(totalCount: number, pageSize: number): number {
  return Math.ceil(totalCount / pageSize);
}

export function formatDateCell(cell: ReactNode) {
  const cellValue = cell as string | number;
  const date = cellValue ? new Date(cellValue) : null;
  return date ? date.toLocaleString() : null;
}

export const roleTranslations: { [key: string]: string } = {
  'Dean': 'Dziekan',
  'Teacher': 'Nauczyciel',
  'WKJK': 'Wydziałowa Komisja Jakości Kształcenia'
};