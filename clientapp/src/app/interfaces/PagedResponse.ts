import { Response } from "./Response";

export interface PagedResponse<T> extends Response<T> {
  pageNumber: number;
  pageSize: number;
  firstPage: string;
  lastPage: string;
  totalPages: number;
  totalRecords: number;
  nextPage: string;
  previousPage: string;
}

