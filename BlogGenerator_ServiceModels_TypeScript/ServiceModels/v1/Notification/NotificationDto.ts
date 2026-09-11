export interface NotificationDto {
  notificationId: number;
  senderUserId: number | null;
  blogId: number | null;
  commentId: number | null;
  notificationType: NotificationType;
  message: string;
  isRead: boolean;
  createdAt: string;
}
