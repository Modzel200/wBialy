export class ShortEventModel{
  Title: string;
  Description: string;
  Image: string;
  EventDate: Date;

  constructor(title: string, desc: string, img: string, date: Date) {
    this.Title=title;
    this.Description=desc;
    this.Image=img;
    this.EventDate=date;
  }
}
