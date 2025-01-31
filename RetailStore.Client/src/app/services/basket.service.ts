import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { BehaviorSubject, map } from 'rxjs';
import { basketActiveModel, basketItemModel, basketModel, BasketTotalsModel } from '../models/basketModel';
import { productModel } from '../models/productModel';
import { deliveryMethodModel } from '../models/deliveryMethodModel';

@Injectable({
    providedIn: 'root'
})
export class BasketService {

    baseUrl: string = environment.apiUrl + 'Basket';
    paymentBaseUrl: string = environment.apiUrl + 'Payments';
    private basketSource: BehaviorSubject<basketModel | null> = new BehaviorSubject<basketModel | null>(null);
    private basketTotalSource: BehaviorSubject<BasketTotalsModel | null> = new BehaviorSubject<BasketTotalsModel | null>(null);
    basket$ = this.basketSource.asObservable();
    basketTotal$ = this.basketTotalSource.asObservable();

    constructor(private http: HttpClient) { }

    getBasket(id: string) {
        let params = new HttpParams();
        params = params.append('id', id);
        return this.http.get<basketModel>(this.baseUrl, { observe: 'response', params }).pipe(
            map((data: HttpResponse<basketModel>) => {
                this.basketSource.next(data.body!);
                this.calculateTotals();
            })
        );
    }

    setBasket(basket: basketModel) {
        this.http.post<basketModel>(this.baseUrl, basket).subscribe(
            (data: basketModel) => {
                this.basketSource.next(data);
                this.calculateTotals();
            }
        )
    }

    getCurrentBasketValue() {
        return this.basketSource.value;
    }

    addItemToBasket(item: productModel, numbItems = 1) {
        const itemToAdd: basketItemModel = this.mapProductItemToBasketItem(item, numbItems);
        const basket = this.getCurrentBasketValue() ?? this.createBasket();
        basket.items = this.addOrUpdateItem(basket.items, itemToAdd, numbItems);
        this.setBasket(basket);
    }

    incrementItemQuantity(item: basketItemModel) {
        const basket = this.getCurrentBasketValue()!;
        const index = basket.items.findIndex(i => i.id === item.id);
        basket.items[index].quantity++;
        this.setBasket(basket);
    }

    decrementItemQuantity(item: basketItemModel) {
        const basket = this.getCurrentBasketValue()!;
        const index = basket!.items.findIndex(i => i.id === item.id);

        if (basket.items[index].quantity > 1) {
            basket!.items[index].quantity--;
        }
        else {
            this.removeItemFromBasket(item);
        }
        this.setBasket(basket);
    }

    removeItemFromBasket(item: basketItemModel) {
        const basket = this.getCurrentBasketValue()!;
        if (basket.items.some(x => x.id == item.id)) {
            basket.items = basket.items.filter(x => x.id != item.id);

            if (basket.items.length > 0) {
                this.setBasket(basket);
            }
            else {
                this.deleteBasket(basket);
            }
        }
    }

    deleteBasket(basket: basketActiveModel) {
        let params = new HttpParams();
        params = params.append('id', basket.id);

        this.http.delete(this.baseUrl, { observe: 'response', params }).subscribe(() => {
            this.basketSource.next(null);
            this.basketTotalSource.next(null);
            localStorage.removeItem('basket_id');
        });
    }

    private addOrUpdateItem(items: basketItemModel[], itemToAdd: basketItemModel, numbItems: number): basketItemModel[] {
        const index = items.findIndex(i => i.id === itemToAdd.id);

        if (index === -1) {
            itemToAdd.quantity = numbItems;
            items.push(itemToAdd);
        }
        else {
            items[index].quantity += 1;
        }

        return items;
    }

    private createBasket(): basketModel {
        const basket = new basketActiveModel();
        localStorage.setItem('basket_id', basket.id);
        return basket;
    }

    private mapProductItemToBasketItem(item: productModel, quantity: number): basketItemModel {
        var output = {
            id: item.id,
            productName: item.name,
            price: item.price,
            quantity: quantity,
            pictureUrl: item.pictureUrl,
            brand: item.productBrand,
            type: item.productType,
        };

        return output;
    }

    private calculateTotals() {
        const basket = this.getCurrentBasketValue();
        if (basket) {
            const shippingPrice = basket.shippingPrice;
            const subtotal = basket.items.reduce((a, b) => (b.price * b.quantity) + a, 0)
            const total = subtotal + shippingPrice;
            var totalObject = { shippingPrice, subtotal, total }
            this.basketTotalSource.next(totalObject);
        }
    }

    setShippingPrice(deliveryMethod: deliveryMethodModel) {
        const basket = this.getCurrentBasketValue();
        if (basket) {
            basket.shippingPrice = deliveryMethod.price;
            basket.deliveryMethodId = deliveryMethod.id;
            this.setBasket(basket);
        }
    }

    createPaymentIntent() {
        return this.http.post<basketModel>(this.paymentBaseUrl +`/${ this.getCurrentBasketValue()?.id}`, {}).pipe(
            map(basket => {
                this.basketSource.next(basket);
            })
        );
    }
}


