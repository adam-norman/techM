import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'requestType'
})
export class RequestTypePipe implements PipeTransform {

  transform(items: any[], id: number): string {
    let request= items.filter(r=>r.id==id)
    return request?request[0].type:'';
  }

}
