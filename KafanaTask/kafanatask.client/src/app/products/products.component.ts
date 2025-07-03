import { Component } from '@angular/core';
import { AllServicesService, Product } from '../Service/all-services.service';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {

  customerId!: number | null;

  products: Product[] = [];

  language: 'en' | 'ar' = 'en';

  constructor(private service: AllServicesService) { }

  ngOnInit() {
    this.loadProducts();
    this.customerId = this.getCustomerIdFromToken();
    console.log('Customer ID:', this.customerId);
  }

  labels = {
    en: {
      toggle: 'عربي',
      viewOrders: 'View My Orders',
      status: 'Status',
      active: 'Active',
      inactive: 'Inactive',
      Amount: 'Amount',
      currency: 'Currency'
    },
    ar: {
      toggle: 'English',
      viewOrders: 'عرض طلباتي',
      status: 'الحالة',
      active: 'نشط',
      inactive: 'غير نشط',
      Amount: 'الكمية',
      currency: 'العملة'
    }
  };

  toggleLanguage() {
    this.language = this.language === 'en' ? 'ar' : 'en';
  }

  loadProducts() {
    this.service.getAllProducts().subscribe({
      next: (data: Product[]) => {
        this.products = data;
      },
      error: (err: any) => {
        console.error('Error loading products', err);
      }
    });
  }

  getCustomerIdFromToken(): number | null {
    const token = localStorage.getItem('jwtToken');

    if (token) {
      try {
        const decodedToken: any = jwtDecode(token);

        
        console.log('Decoded Token:', JSON.stringify(decodedToken, null, 2));

       
        if (decodedToken.nameid) {
          return +decodedToken.nameid;
        }

     
        if (decodedToken.sub) {
          return +decodedToken.sub;
        }

        return null;

      } catch (error) {
        console.error('Error decoding token:', error);
        return null;
      }
    }
    return null;
  }
}
