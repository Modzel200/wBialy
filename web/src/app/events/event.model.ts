export class EventPost{
  public PostId: number;
  public Title: string;
  public Description: string;
  public Image: string;
  public AddDate: Date;
  public Place: string;
  public UserId: number;
  public Confirmed: boolean;
  public Link: string;
  public EventDate: Date;
  public EventTags: string[]

  constructor(id:number, title:string, desc:string, img:string, addDate:Date,place:string,userId:number,confirmed:boolean,link:string,eventDate:Date,eventTags:string[]) {
    this.PostId=id;
    this.Title = title
    this.Description = desc;
    this.Image = img;
    this.AddDate = addDate;
    this.Place = place;
    this.UserId = userId;
    this.Confirmed = confirmed;
    this.Link = link;
    this.EventDate = eventDate;
    this.EventTags = eventTags;
  }
}
