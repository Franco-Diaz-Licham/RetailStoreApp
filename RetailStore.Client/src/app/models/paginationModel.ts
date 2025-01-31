import { productModel } from "./productModel";

export interface paginationModel {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: productModel[]
}

