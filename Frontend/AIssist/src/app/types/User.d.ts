export interface User {
  id: number;
  name: string;
  username: string;
  email: string;
  profileName: string;
}

export interface AuthResponse {
  refreshToken: string;
  accessToken: string;
  username: string;
  user: User;
}
