import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../../services/basket.service';
import { basketItemModel, basketModel } from '../../../models/basketModel';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { Route, Router, RouterLink } from '@angular/router';
import { OrderTotalsComponent } from '../order-totals/order-totals.component';
import { BasketSummaryComponent } from '../basket-summary/basket-summary.component';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule, RouterLink, OrderTotalsComponent],
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.css'
})
export class BasketComponent implements OnInit {
    basket$?: Observable<basketModel | null>;

    constructor(private basketService: BasketService, private router: Router){

    }

    ngOnInit(): void {
        this.basket$ = this.basketService.basket$;
    }

    removeBasketItem(item: basketItemModel){
        this.basketService.removeItemFromBasket(item);
    }

    incrementItemQuantity(item: basketItemModel){
        this.basketService.incrementItemQuantity(item);
    }

    decrementItemQuantity(item: basketItemModel){
        this.basketService.decrementItemQuantity(item);
    }
}
