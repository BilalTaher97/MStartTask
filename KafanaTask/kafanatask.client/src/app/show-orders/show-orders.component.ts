import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AllServicesService } from '../Service/all-services.service';

@Component({
  selector: 'app-show-orders',
  templateUrl: './show-orders.component.html',
  styleUrl: './show-orders.component.css'
})
export class ShowOrdersComponent {


  constructor(
    private route: ActivatedRoute,
    private service: AllServicesService
  ) { }

  ngOnInit() {
    
    this.customerId = +this.route.snapshot.paramMap.get('id')!;
    console.log('Customer ID:', this.customerId);

    
    this.loadOrders();
  }

  customerId!: number;
  orders: any[] = [];

  isArabic = false;

  

  toggleLanguage() {
    this.isArabic = !this.isArabic;

    
    this.orders = this.orders.map((order) => ({
      ...order,
      status: this.isArabic
        ? order.status === 'Active'
          ? 'نشط'
          : 'غير نشط'
        : order.status === 'نشط'
          ? 'Active'
          : 'Inactive',
    }));
  }

  loadOrders() {
    this.service.getOrdersByCustomerId(this.customerId).subscribe({
      next: (data) => {
        this.orders = data;
      },
      error: (err) => {
        console.error('Error loading orders', err);
      }
    });
  }


  cancelOrder(orderId: number) {
    this.service.cancelOrder(orderId).subscribe({
      next: res => {
        console.log('Order cancelled:', res);
       
        this.loadOrders();
      },
      error: err => {
        console.error('Error cancelling order:', err);
      }
    });
  }


}
