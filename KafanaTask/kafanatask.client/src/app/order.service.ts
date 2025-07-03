import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private _http: HttpClient) { }


  createOrder(orderPayload: any): Observable<any> {
    return this._http.post('https://localhost:7086/api/Admin/orders', orderPayload);
  }

}
