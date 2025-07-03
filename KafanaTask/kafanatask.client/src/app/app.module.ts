import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { ProductsComponent } from './products/products.component';
import { ShowOrdersComponent } from './show-orders/show-orders.component';
import { SidebarComponent } from './Admin/sidebar/sidebar.component';
import { ProductListComponent } from './Admin/product-list/product-list.component';
import { CustomerListComponent } from './Admin/customer-list/customer-list.component';
import { OrderListComponent } from './Admin/order-list/order-list.component';
import { AddProductComponent } from './Admin/add-product/add-product.component';
import { CreateOrdersComponent } from './create-orders/create-orders.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    ProductsComponent,
    ShowOrdersComponent,
    SidebarComponent,
    CustomerListComponent,
    ProductListComponent,
    OrderListComponent,
    AddProductComponent,
    CreateOrdersComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
