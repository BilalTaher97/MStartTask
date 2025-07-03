import { Component } from '@angular/core';
import { OrderService } from '../order.service';

@Component({
  selector: 'app-create-orders',
  templateUrl: './create-orders.component.html',
  styleUrl: './create-orders.component.css'
})
export class CreateOrdersComponent {

  constructor(private _orderService: OrderService) { }




}
