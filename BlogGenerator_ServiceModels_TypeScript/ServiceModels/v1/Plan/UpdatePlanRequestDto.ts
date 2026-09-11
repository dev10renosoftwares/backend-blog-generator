export interface UpdatePlanRequestDto {
  name: string;
  description: string | null;
  price: number;
  credits: number;
  isActive: boolean;
}
