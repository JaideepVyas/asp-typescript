export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  fullName: string;
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
}

/* ✅ ADD THIS ONLY */

export interface UserResponseDto {
  id: string;
  fullName: string;
  email: string;
}