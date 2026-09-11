export interface FeedbackDto {
  feedbackId: number;
  userId: number;
  subject: string;
  message: string;
  rating: number;
  isPublic: boolean;
  status: FeedbackStatus;
  adminResponse: string | null;
  createdAt: string;
  updatedAt: string | null;
}
