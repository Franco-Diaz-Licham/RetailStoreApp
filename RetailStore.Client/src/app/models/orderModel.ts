import { addressModel } from "./userModel";

export interface orderToCreateModel {
    basketId: string;
    deliveryMethodId: number;
    shipToAddress: addressModel;
}

export interface orderModel {
    id: number;
    buyerEmail: string;
    orderDate: string;
    shipToAddress: addressModel;
    deliveryMethod: string;
    shippingPrice: number;
    orderItems: orderItemModel[];
    subtotal: number;
    total: number;
    status: string;
  }

  export interface orderItemModel {
    productId: number;
    productName: string;
    pictureUrl: string;
    price: number;
    quantity: number;
  }
