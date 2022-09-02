export const hostUrl = process.env.REACT_APP_HOST_URL;

export const baseUrl = `${hostUrl}/api`;

export const loginApi = `${baseUrl}/v1/login`;
export const tasksApi = `${baseUrl}/v1/tasks`;
export const usersApi = `${baseUrl}/v1/users`;

export const dashboardApi = `${baseUrl}/v1/dashboard`;

export const messagingConfigurationApi = `${baseUrl}/v1/configuration/messaging`;
