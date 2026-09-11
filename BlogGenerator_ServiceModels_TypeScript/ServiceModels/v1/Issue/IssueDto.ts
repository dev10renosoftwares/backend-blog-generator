export interface IssueDto {
  issueId: number;
  userId: number;
  subject: string;
  description: string;
  status: IssueStatus;
  createdAt: string;
  updatedAt: string | null;
}
