export interface TaskResponse {
  Id: string;
  Title: string;
  Description: string;
  Status: number;
  Priority: number;
  AssignedUserId: string;
  AssignedUserFirstName: string;
  AssignedUserLastName: string;
  DueDate: Date;
  PercentageCompleted: number;
  IsOverDue: boolean;
  IsPinned: boolean;
}
