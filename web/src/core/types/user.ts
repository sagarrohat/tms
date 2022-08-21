export interface LoginResponse {
  UserId: string;
  FirstName: string;
  LastName: string;
  EmailAddress: string;
  Secret: string;
  Expiry: Date;
}