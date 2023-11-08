export interface PostToAdd{
  postId:number;
  title: string;
  description: string;
  image: string;
  place: string;
  location: string;
  eventDate: string;
  tags: Tags[];
  link: string;
}
export interface Tags{
  name: string;
}

export interface lfPostToAdd{
  postId:number;
  title: string;
  description: string;
  image: string;
  place: string;
  location: string;
  tags: Tags[];
}
export interface Tags{
  name: string;
}

export interface gastroPostToAdd{
  postId:number;
  title: string;
  description: string;
  image: string;
  place: string;
  location: string;
  day: string;
  tags: Tags[];
  link: string;
}
export interface Tags{
  name: string;
}
