import { AdminUserDto } from './AdminUserDto';

export interface AdminUserDetailsDto extends AdminUserDto {
  totalBlogs: number;
  totalLikes: number;
  totalComments: number;
  totalPayments: number;
}
