import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProductsComponent } from './products/products.component';
import { ShowOrdersComponent } from './show-orders/show-orders.component';

const routes: Routes = [
  { path: '', redirectTo: '/Login', pathMatch: 'full' },
  { path: "Login", component: LoginComponent },
  { path: "register", component: RegisterComponent },
  { path: "products", component: ProductsComponent },
  { path: 'ShowOrders/:id', component: ShowOrdersComponent },
  {
    path: "admin",
    component: SidebarComponent,
    children: [
      { path: "", redirectTo: "customers", pathMatch: "full" },
      { path: "customers", component: CustomerListComponent },
      { path: "products", component: ProductListComponent },
      { path: "orders", component: OrderListComponent }
    ]
  },

  {
    path: '',
    redirectTo: 'admin',
    pathMatch: 'full'
  },

  {
    path: '**',
    redirectTo: 'admin'
  }

];

import { SidebarComponent } from './Admin/sidebar/sidebar.component';
import { CustomerListComponent } from './Admin/customer-list/customer-list.component';
import { ProductListComponent } from './Admin/product-list/product-list.component';
import { OrderListComponent } from './Admin/order-list/order-list.component';





@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

  
}
