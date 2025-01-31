import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { BasketService } from '../../../services/basket.service';
import { basketModel } from '../../../models/basketModel';
import { Observable } from 'rxjs';
import { AsyncPipe, NgIf } from '@angular/common';
import { userModel } from '../../../models/userModel';
import { AccountService } from '../../../services/account.service';

@Component({
    selector: 'app-nav-bar',
    standalone: true,
    imports: [RouterLink, RouterLinkActive, NgIf, AsyncPipe],
    templateUrl: './nav-bar.component.html',
    styleUrl: './nav-bar.component.css'
})
export class NavBarComponent implements OnInit {

    basket$?: Observable<basketModel | null>;
    currentUser$?: Observable<userModel | null>;

    constructor(private router: Router, private basketService: BasketService, private accountService: AccountService){ }

    ngOnInit(): void {
        this.basket$ = this.basketService.basket$;
        this.currentUser$ = this.accountService.currentUser$;
    }

    navigateToShop(){
        this.router.navigateByUrl("/shop");
    }

    logOut(){
        this.accountService.logout();
    }
}
