import { v4 as uuidv4 } from 'uuid';

export interface basketModel {
    id: string;
    items: basketItemModel[],
    clientSecret?: string;
    paymentIntentId?: string;
    deliveryMethodId?: number;
    shippingPrice: number;
}

export class basketActiveModel implements basketModel {
    id: string = uuidv4();
    items: basketItemModel[] = [];
    shippingPrice = 0;
}

export interface basketItemModel {
    id: number;
    productName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    brand: string;
    type: string;
}

export interface BasketTotalsModel {
    shippingPrice: number;
    subtotal: number;
    total: number;
}