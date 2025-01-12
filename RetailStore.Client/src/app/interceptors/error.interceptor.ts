import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, Observable } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private router: Router, private toaster: ToastrService) { }
    
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
        var output = next.handle(req).pipe(
            catchError((error: HttpErrorResponse) => {
                if(error){
                    if(error.status === 400) this.handleBadRequest(error); 
                    else if(error.status === 401) this.handleUnauthorized(error); 
                    else if(error.status === 404)  this.handleNotFound(error);
                    else if(error.status === 500) this.handleInternalError(error); 
                    else this.handleGeneric();
                }
                
                throw error;
            })
        );

        return output;
    }

    handleBadRequest(error: HttpErrorResponse){
        // validation errors from UI
        if (error.error.errors) {
            const modelStateErrors = [];
            for (const key in error.error.errors) {
                if (error.error.errors[key]) modelStateErrors.push(error.error.errors[key]) 
            }
            this.toaster.error(error.error.message, error.status.toString());
            throw modelStateErrors.flat();
        }
        // object errors coming from API
        else if (error.error.validationErrors) {
            var errors = error.error.validationErrors.flat();
            this.toaster.error(errors, error.status.toString());
        }
        // generic error
        else {
            this.toaster.error('Not a good request', error.status.toString());
        }
    }

    handleNotFound(error: HttpErrorResponse){
        this.toaster.warning(error.error.message, error.status.toString());
    }

    handleUnauthorized(error: HttpErrorResponse){
        this.toaster.error('Unauthorized', error.status.toString());
    }

    handleInternalError(error: HttpErrorResponse){
        const navigationExtras: NavigationExtras = { 
            state: { 
                error: error.error 
            } 
        };
        this.router.navigateByUrl('/server-error', navigationExtras);
    }

    handleGeneric(){
        this.toaster.error('Something went wrong...');
    }
}
