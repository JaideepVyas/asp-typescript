import * as authApi from "../api/authApi";

import type {
  LoginRequest,
  LoginResponse,
  RegisterRequest,
} from "../types/auth";

export const login = async (
  data: LoginRequest
): Promise<LoginResponse> => {
  return await authApi.login(data);
};

export const register = async (
  data: RegisterRequest
) => {
  return await authApi.register(data);
};