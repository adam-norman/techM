
export interface RequestFormViewModel {
  id: number;
  subject: string;
  body: string;
  requestDate: string;
  hasApproved: number | null;
  requestTypeId: number;
  requestType: string | null;
  employeeName: string | null;
  employeeId: string | null;
}
