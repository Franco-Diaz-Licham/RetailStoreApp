import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ShopMainComponent } from './components/shop/shop-main/shop-main.component';
import { ProductDetailsComponent } from './components/shop/product-details/product-details.component';
import { NotFoundComponent } from './components/errors/not-found/not-found.component';
import { ProductDetailsResolver } from './resolvers/product-details.resolver';

export const routes: Routes = [
   {path: '', component: HomeComponent},
   {path: 'shop', component: ShopMainComponent},
   {path: 'shop/:id', component: ProductDetailsComponent, resolve: { product: ProductDetailsResolver } },
   { path: '**', component: NotFoundComponent, pathMatch: 'full' }
];
