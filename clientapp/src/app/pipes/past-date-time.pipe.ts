import { Pipe, PipeTransform } from '@angular/core';
import dateFormat, { masks } from "dateformat";
@Pipe({
  name: 'pastDateTime'
})
export class PastDateTimePipe implements PipeTransform {

  transform(OrigDate: string, ...args: unknown[]): string {
  const currentDate = new Date();
  const passedDate = new Date(OrigDate);
  const differenceInTime = currentDate.getTime() - passedDate.getTime();
  const days=  differenceInTime / (1000 * 3600 * 24);
  if(days<365 && days>=1)
      {
        return  dateFormat(passedDate, "mmm, yyyy").toString();
      }
      if(days>=365)
      {
        return  dateFormat(passedDate, "d - mmm - yyyy").toString();
      }
      const hours=days*24
      if(hours>1)
      {
        return  Math.round(hours).toString()+' hrs ago';
      }
      const minutes=hours*60;
      if(minutes>1)
      {
        return  Math.round(minutes).toString()+' min ago';
      }
      else{
        return 'now';
      }
  }
}
