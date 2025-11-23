export interface User {
  id: number;
  name: string;
  username: string;
  email: string;
  profile: string;
}

export interface UserResponse {
  id: number;
  name: string;
  username: string;
  email: string;
  active: number;
  profile: number;
}

export interface Technician {
  id: number;
  name: string;
  username: string;
  email: string;
  active: boolean;
  profileId: number;
}