// src/app/models/loginResponse.ts
export interface User {
  name: string;
  email: string;
  id: string;
  userName: string;
  // Puedes agregar más propiedades si las necesitas
}

export interface LoginResponse {
  statusCode: number;
  isSuccess: boolean;
  errorMessages: string[];
  result: {
    token: string;
    user: User;
  };
}
