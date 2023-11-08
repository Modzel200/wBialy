export interface lfPost{
    postId: number;
    title: string;
    description: string;
    image: string;
    place: string;
    location: string;
    confirmed: boolean;
    tags: Tags[]
  }
  export interface Tags{
    name: string;
  }
  