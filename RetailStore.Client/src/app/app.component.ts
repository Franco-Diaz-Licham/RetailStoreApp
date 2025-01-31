import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from './components/layout/nav-bar/nav-bar.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SectionHeaderComponent } from "./components/layout/section-header/section-header.component";
import { BasketService } from './services/basket.service';
import { AccountService } from './services/account.service';


@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet, NavBarComponent, NgxSpinnerModule, SectionHeaderComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
    constructor(private basketService: BasketService, private accountService: AccountService) {

    }

    ngOnInit(): void {
        this.loadBasket();
        this.loadUser();
    }

    loadBasket(){
        const basketId = localStorage.getItem('basket_id');

        if(basketId){
            this.basketService.getBasket(basketId).subscribe();
        }
    }

    loadUser(){
        const token = localStorage.getItem('token');

        if(token){
            this.accountService.loadCurrentUser(token).subscribe();
        }
    }
}
