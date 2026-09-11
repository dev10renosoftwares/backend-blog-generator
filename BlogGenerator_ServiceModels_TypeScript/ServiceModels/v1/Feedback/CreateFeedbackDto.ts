export interface CreateFeedbackDto {
  subject: string;
  message: string;
  rating: number;
  isPublic: boolean;
}
