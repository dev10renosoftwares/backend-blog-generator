export interface CreatePlanRequestDto {
  name: string;
  description: string | null;
  price: number;
  credits: number;
}
