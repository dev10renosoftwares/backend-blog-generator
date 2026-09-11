export interface DeletedUserDto {
  deletedAccountId: number;
  userId: number;
  email: string;
  reason: string | null;
  deletedAt: string;
}
