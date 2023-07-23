import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class SharedService {
  private sharedObject: any;
  public IsLoggegIn: boolean=false;

  setSharedObject(object: any): void {
    this.sharedObject = object;
  }

  getSharedObject(): any {
    return this.sharedObject;
  }
}