import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {

    // number of HTTP requests
    busyRequestCount: number = 0;
    constructor(private spinnerService: NgxSpinnerService) { }

    busy(){
        this.busyRequestCount++;
        this.spinnerService.show(undefined, {
            type: "ball-spin-fade",
            size: "large",
            bdColor: "rgba(147, 147, 147, 0.5)",
            color: "black",
            
        })
    }

    idle(){
        this.busyRequestCount--;
        if(this.busyRequestCount <= 0){
            this.busyRequestCount = 0;
            this.spinnerService.hide();
        }
    }

}
