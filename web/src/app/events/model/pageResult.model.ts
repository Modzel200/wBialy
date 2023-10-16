import {EventPost} from "./event.model";

export interface PageResultModel{
  items: EventPost[],
  totalPages: number,
  itemFrom: number,
  itemTo: number,
  totalItemsCount: number
}
