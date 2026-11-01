export interface PaginatedResult<T> {
  items: T[];
  totalCount: number;
  totalPages: number;
  itemsFrom: number;
  itemsTo: number;
}
