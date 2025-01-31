import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';
import { map, Observable } from 'rxjs';
import { userModel } from '../models/userModel';

@Injectable({
    providedIn: 'root'
})

export class AuthGuard implements CanActivate{

    constructor(private accService: AccountService,  private router: Router){}

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        return this.accService.currentUser$.pipe(
            map((user: userModel | null) => {
                if(user){
                    return true;
                }
                else{
                    this.router.navigate(['account/login'], { queryParams: {returnUrl: state.url}});
                    return false;
                }
            })
        );
    }
}