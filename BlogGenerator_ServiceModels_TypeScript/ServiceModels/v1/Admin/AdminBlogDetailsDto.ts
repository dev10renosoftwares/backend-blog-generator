import { AdminBlogDto } from './AdminBlogDto';

export interface AdminBlogDetailsDto extends AdminBlogDto {
  content: string;
  views: number;
  likes: number;
  comments: number;
  reposts: number;
}
