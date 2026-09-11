export interface PlanResponseDto {
  planId: number;
  name: string;
  description: string | null;
  price: number;
  credits: number;
  isActive: boolean;
  createdAt: string;
}
