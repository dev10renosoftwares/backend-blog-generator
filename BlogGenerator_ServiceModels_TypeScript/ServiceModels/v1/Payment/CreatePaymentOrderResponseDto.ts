export interface CreatePaymentOrderResponseDto {
  success: boolean;
  message: string;
  paymentId: number;
  razorpayKeyId: string;
  razorpayOrderId: string;
  amount: number;
  currency: string;
}
