import axiosClient from "./axiosClient";

import type {
  LoginRequest,
  LoginResponse,
  RegisterRequest,
} from "../types/auth";

export const login = async (
  data: LoginRequest
): Promise<LoginResponse> => {
  const response = await axiosClient.post(
    "/users/login",
    data
  );

  return response.data;
};

export const register = async (
  data: RegisterRequest
) => {
  const response = await axiosClient.post(
    "/users",
    data
  );

  return response.data;
};

// ✅ ADDED
export const getUsers = async () => {
  const response = await axiosClient.get(
    "/users"
  );

  return response.data;
};