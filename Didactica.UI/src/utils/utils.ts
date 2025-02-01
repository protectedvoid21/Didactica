export function getPageCount(totalCount: number, pageSize: number): number {
  return Math.ceil(totalCount / pageSize);
}