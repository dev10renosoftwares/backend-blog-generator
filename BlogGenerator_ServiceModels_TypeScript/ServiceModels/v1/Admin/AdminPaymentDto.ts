export interface AdminPaymentDto {
  paymentId: number;
  userId: number;
  userName: string;
  amount: number;
  status: string;
  razorpayOrderId: string;
  razorpayPaymentId: string;
  createdAt: string;
}
