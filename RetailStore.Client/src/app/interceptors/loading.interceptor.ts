import { HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BusyService } from '../services/busy.service';
import { delay, finalize, Observable } from 'rxjs';

@Injectable()
export class loadingInterceptor implements HttpInterceptor{

    constructor(private busyService: BusyService){ }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if(req.method === 'POST' && req.url.includes('orders')){
            return next.handle(req);
        }

        this.busyService.busy();

        return next.handle(req).pipe(
            delay(1000),
            finalize(() => {
                this.busyService.idle();
            })
        );
    }
}