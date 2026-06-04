import axiosClient from "./axiosClient";
import {
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