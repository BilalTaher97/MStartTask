import { Component, OnInit } from '@angular/core';
import { AdminServiceService, Order } from '../../AdminService/admin-service.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrl: './order-list.component.css'
})
export class OrderListComponent implements OnInit {

  orders: Order[] = [];
  currentPage = 1;

  constructor(private adminService: AdminServiceService) { }


  ngOnInit() {
    this.loadOrders();
  }


  loadOrders() {
    this.adminService.getOrders(this.currentPage, 10).subscribe(response => {
      this.orders = response.data;
    })
  }


  nextPage() {
    this.currentPage++;
    this.loadOrders();
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadOrders();
    }
  }

}
