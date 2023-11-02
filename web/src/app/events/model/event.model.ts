export interface EventPost{
  //public PostId: number;
  postId: number;
  title: string;
  description: string;
  image: string;
  place: string;
  location: string;
  eventDate: string;
  confirmed: boolean;
  day: string;
  tags: Tags[]
  link: string;
}
export interface Tags{
  name: string;
}
