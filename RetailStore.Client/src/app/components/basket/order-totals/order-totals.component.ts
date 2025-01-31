import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../../services/basket.service';
import { Observable } from 'rxjs';
import { BasketTotalsModel } from '../../../models/basketModel';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-order-totals',
  standalone: true,
  imports: [ CommonModule],
  templateUrl: './order-totals.component.html',
  styleUrl: './order-totals.component.css'
})
export class OrderTotalsComponent implements OnInit {

    basketTotal$?: Observable<BasketTotalsModel | null>;

    constructor(private basketService: BasketService, private router: Router){ }

    ngOnInit(): void {
        this.basketTotal$ = this.basketService.basketTotal$;
    }
}
