export interface ApiResponse {
  success: boolean;
  message: string;
  data: T | null;
  errors: string[] | null;
}
