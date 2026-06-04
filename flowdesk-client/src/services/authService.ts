import * as authApi from "../api/authApi";
import {
  LoginRequest,
  RegisterRequest,
} from "../types/auth";

export const login = async (
  data: LoginRequest
) => {
  const result = await authApi.login(data);

  localStorage.setItem(
    "token",
    result.token
  );

  return result;
};

export const register = async (
  data: RegisterRequest
) => {
  return await authApi.register(data);
};

export const logout = () => {
  localStorage.removeItem("token");
};

export const isAuthenticated = () => {
  return !!localStorage.getItem("token");
};