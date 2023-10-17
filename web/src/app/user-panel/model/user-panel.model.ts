export interface PostToAdd{
  title: string;
  description: string;
  image: string;
  place: string;
  day: string;
  tags: Tags[];
  link: string;
}
export interface Tags{
  name: string;
}
