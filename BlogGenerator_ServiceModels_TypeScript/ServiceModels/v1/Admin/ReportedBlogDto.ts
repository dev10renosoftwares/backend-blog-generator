export interface ReportedBlogDto {
  reportId: number;
  blogId: number;
  blogTitle: string;
  reportedByUserId: number;
  reportedByUserName: string;
  reason: string;
  status: string;
  createdAt: string;
}
