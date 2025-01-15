import { v4 as uuidv4 } from 'uuid';

export interface basketModel {
    id: string;
    items: basketItemModel[]
}

export class basketActiveModel implements basketModel {
    id: string = uuidv4();
    items: basketItemModel[] = [];
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
    shipping: number;
    subtotal: number;
    total: number;
}
