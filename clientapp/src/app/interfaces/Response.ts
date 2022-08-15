
export interface Response<T> {
  data: T;
  succeeded: boolean;
  errors: string[];
  message: string;
}
