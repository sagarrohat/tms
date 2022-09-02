export type LoginRequest = {
  emailAddress: string;
  pd: string;
};

export type LoginResponse = {
  UserId: string;
  FirstName: string;
  LastName: string;
  EmailAddress: string;
  Secret: string;
  Expiry: Date;
};

export type LoginAlert = {
  message: string;
  type: "success" | "info" | "warning" | "error";
};
