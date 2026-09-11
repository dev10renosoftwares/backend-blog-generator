export interface AdminUserDto {
  userId: number;
  userName: string;
  email: string;
  role: string;
  profilePictureUrl: string | null;
  availableCredits: number;
  isBlocked: boolean;
  createdAt: string;
}
