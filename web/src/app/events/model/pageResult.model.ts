import {EventPost} from "./event.model";

export interface PageResultModel{
  Items: EventPost[],
  TotalPages: number,
  ItemFrom: number,
  ItemTo: number,
  TotalItemsCount: number
}
