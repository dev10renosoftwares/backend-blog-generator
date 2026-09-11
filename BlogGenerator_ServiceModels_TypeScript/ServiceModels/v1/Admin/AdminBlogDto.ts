export interface AdminBlogDto {
  blogId: number;
  userId: number;
  userName: string;
  title: string;
  excerpt: string | null;
  isPublished: boolean;
  createdAt: string;
  publishedAt: string | null;
}
