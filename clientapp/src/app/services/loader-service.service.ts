import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderServiceService {
 isLoading:BehaviorSubject<boolean> = new  BehaviorSubject<boolean>(false);
  constructor() { }
}
