import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ShopMainComponent } from './components/shop/shop-main/shop-main.component';
import { ProductDetailsComponent } from './components/shop/product-details/product-details.component';
import { NotFoundComponent } from './components/errors/not-found/not-found.component';
import { ProductDetailsResolver } from './resolvers/product-details.resolver';
import { ServerErrorComponent } from './components/errors/server-error/server-error.component';
import { TestErrorsComponent } from './components/errors/test-errors/test-errors.component';

export const routes: Routes = [
    { path: '', component: HomeComponent, data: { breadcrumb: 'Home'} },
    { path: 'shop', children: [
        { path: '', pathMatch: 'full', component: ShopMainComponent, data: { breadcrumb: 'Shop'}},
        { path: ':id', component: ProductDetailsComponent, resolve: { product: ProductDetailsResolver }, data: { breadcrumb: { alias: 'productDetails'} } }
    ]},    
    { path: 'not-found', component: NotFoundComponent , data: { breadcrumb: 'Not Found'} },
    { path: 'server-error', component: ServerErrorComponent, data: { breadcrumb: 'Server Error'}  },
    { path: "errors", component: TestErrorsComponent , data: { breadcrumb: 'Test Error'} },
    { path: '**', component: NotFoundComponent, pathMatch: 'full', data: { breadcrumb: 'Not Found'}  }
]
