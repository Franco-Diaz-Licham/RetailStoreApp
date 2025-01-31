import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BasketService } from '../../../services/basket.service';
import { basketItemModel } from '../../../models/basketModel';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'app-basket-summary',
    standalone: true,
    imports: [CommonModule, RouterLink],
    templateUrl: './basket-summary.component.html',
    styleUrl: './basket-summary.component.css'
})
export class BasketSummaryComponent {
    
    @Output() addItem = new EventEmitter<basketItemModel>();
    @Output() removeItem = new EventEmitter<{ id: number, quantity: number }>();
    @Input() isBasket: boolean = false;

    constructor(public basketService: BasketService) { }
}
