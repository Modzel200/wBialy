export interface EventPost {
  //public PostId: number;
  postId: number;
  title: string;
  description: string;
  image: string;
  place: string;
  found: boolean;
  location: string;
  eventDate: string;
  confirmed: boolean;
  day: string;
  tags: Tags[]
  link: string;
  isLiked: boolean;
  likeCount: number;
}
export interface Tags {
  name: string;
}
