export interface AdminFeedbackDto {
  feedbackId: number;
  userId: number;
  userName: string;
  message: string;
  isResolved: boolean;
  createdAt: string;
}
