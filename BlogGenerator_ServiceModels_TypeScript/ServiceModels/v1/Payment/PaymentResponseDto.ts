export interface PaymentResponseDto {
  paymentId: number;
  userId: number;
  planId: number;
  amount: number;
  creditsPurchased: number;
  razorpayOrderId: string;
  razorpayPaymentId: string | null;
  paymentStatus: PaymentStatus;
  purchasedAt: string;
}
