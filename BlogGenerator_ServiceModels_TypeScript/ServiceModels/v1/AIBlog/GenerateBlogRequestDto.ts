export interface GenerateBlogRequestDto {
  categoryId: number;
  topic: string;
  audience: BlogAudience;
  tone: BlogTone;
  wordCount: BlogWordCount;
  language: BlogLanguage;
}
