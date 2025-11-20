export interface AuthResponse {
  refreshToken: string;
  accessToken: string;
  username: string;
  id: number;
  email: string;
  name: string;
  profile: string;
}