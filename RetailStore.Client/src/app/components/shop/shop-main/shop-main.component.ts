import { Component } from '@angular/core';
import { ProductItemComponent } from "../product-item/product-item.component";
import { productModel } from '../../../models/productModel';
import { ProductService } from '../../../services/product.service';
import { paginationModel } from '../../../models/paginationModel';
import { ProductTypeService } from '../../../services/product-type.service';
import { ProductBrandService } from '../../../services/product-brand.service';
import { productTypeModel } from '../../../models/productTypeModel';
import { productBrandModel } from '../../../models/productBrandModel';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { productParams } from '../../../models/productParams';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@Component({
    selector: 'app-shop-main',
    standalone: true,
    imports: [ProductItemComponent, CommonModule, FormsModule, PaginationModule],
    templateUrl: './shop-main.component.html',
    styleUrl: './shop-main.component.css'
})
export class ShopMainComponent {

    products: productModel[] = [];
    productTypes: productTypeModel[] = [];
    productBrands: productBrandModel[] = [];
    totalCount: number = 0;
    sortOptions = [
        {name: 'Select sort order...', value: ''},
        {name: 'Alphabetical', value: 'name'},
        {name: 'Price: Low to High', value: 'priceAsc'},
        {name: 'Price: High to Low', value: 'priceDesc'}
    ]
    productParams: productParams = new productParams();

    constructor(
        private productService: ProductService,
        private productTypeService: ProductTypeService,
        private productBrandService: ProductBrandService) { }

    ngOnInit(): void {
        this.getData();
    }

    getData() {
        this.getProducts(this.productParams);
        this.getProductBrands();
        this.getProductTypes();
    }

    getProducts(productParams: productParams) {
        this.productService.getProducts(productParams).subscribe({
            next: (resp: paginationModel) => {
                this.productParams.pageIndex = resp.pageIndex;
                this.productParams.pageSize = resp.pageSize;
                this.totalCount = resp.count;
                this.products = resp.data;
            }
        });
    }

    getProductTypes() {
        this.productTypeService.getProductTypes().subscribe({
            next: (resp: productTypeModel[]) => {
                this.productTypes = [{id: 0, name: "All"}, ...resp];
            }
        });
    }

    getProductBrands() {
        this.productBrandService.getProductBrands().subscribe({
            next: (resp: productBrandModel[]) => {
                this.productBrands = [{id: 0, name: "All"}, ...resp];
            }
        });
    }

    handleProductBrandChanged(productBrand: productBrandModel) {
        this.productParams.brandId = productBrand.id;
    }

    handleProductTypeChanged(productType: productTypeModel) {
        this.productParams.typeId = productType.id;
    }

    handleApplyFilterChanged() {
        this.productParams.pageIndex = 1;
        this.getProducts(this.productParams);
    }

    handleSortSelected(sort: any){
        console.log(sort)
        this.productParams.sort = sort;
    }
    
    handleClearFilterChanged() {
        this.productParams = new productParams();
        this.getProducts(this.productParams);
    }

    handlePageChanged(event: any) {
        if(this.productParams.pageIndex === event.page) return;
        this.productParams.pageIndex = event.page;
        this.getProducts(this.productParams);
    }

    getItemsShowing() : string {
        
        if(this.totalCount === 0) return '0';

        const lower = (this.productParams.pageIndex - 1 )* this.productParams.pageSize + 1;
        const upper = this.productParams.pageIndex*this.productParams.pageSize;
        const offset = upper > this.totalCount ? upper - this.totalCount : 0;
        return `${lower} - ${upper - offset}`;
    }

    handleSearch(){
        this.productParams.pageIndex = 1;
        this.getProducts(this.productParams);
    }

    clearSearch() {
        this.productParams.search = '';
        this.getProducts(this.productParams);
    }
}
