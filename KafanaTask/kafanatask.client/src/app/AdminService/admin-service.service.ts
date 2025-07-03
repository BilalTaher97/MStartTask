import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';



export interface Customer {
  id: number;
  serverDateTime: string;
  dateTimeUtc: string;
  updateDateTimeUtc: string | null;
  lastLoginDateTimeUtc: string | null;
  nameEn: string;
  nameAr: string;
  email: string;
  phone: string;
  statusEn: string;
  statusAr: string;
  genderEn: string;
  genderAr: string;
  dateOfBirth: string;
  photo: any;
  orders: any[];

  OrdersCount: number
}



export interface PagedCustomerResponse {
  data: Customer[];
  total: number;
  currentPage: number;
  pageSize: number;
}


export interface Product {
  id: number;
  serverDateTime: string;
  dateTimeUtc: string;
  updateDateTimeUtc: string | null;
  nameEn: string;
  nameAr: string;
  descriptionEn: string;
  descriptionAr: string;
  statusEn: string;
  statusAr: string;
  amount: number;
  currency: string;
  orders: any[]; 
}


export interface PagedProductResponse {
  data: Product[];
  total: number;
  currentPage: number;
  pageSize: number;
}


export interface ProductCreateDto {
  nameEn: string;
  nameAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  statusEn: string;
  statusAr: string;
  amount: number;
  currency: string;
}


export interface Order {
  id: number;
  customerId: number;
  productId: number;
  serverDateTime: string;
  dateTimeUtc: string;
  totalAmount: number;
  currency: string;
  statusEn: string;
  statusAr: string;
  customer: any | null;
  product: any | null;
}


export interface pagedOrderRespose {
  data: Order[],
  totalPage: number,
  currentPage: number,
  pageSize: number
}


@Injectable({
  providedIn: 'root'
})



export class AdminServiceService {

  private userApiUrl = 'https://localhost:7152/api/Admin/customers';
  private productApiUrl = 'https://localhost:7152/api/Admin/products';
  private AddProductApiUrl = 'https://localhost:7152/api/Admin/add-product';
  private orderApiUrl = 'https://localhost:7152/api/Admin/orders';

  private translateUrl = 'https://libretranslate.de/translate';


  constructor(private http: HttpClient) { }

  getCustomers(page: number = 1, pageSize: number = 10): Observable<PagedCustomerResponse> {
    return this.http.get<PagedCustomerResponse>(`${this.userApiUrl}?page=${page}&pageSize=${pageSize}`);
  }

  getCustomerById(id: number): Observable<Customer> {
    return this.http.get<Customer>(`${this.userApiUrl}/${id}`);
  }


  deleteCustomers(ids: number[]): Observable<any> {
    return this.http.request('delete', this.userApiUrl, {
      body: ids
    });
  }



  updateCustomerStatus(id: number, statusEn: string, statusAr: string): Observable<any> {
    const url = `https://localhost:7152/api/Admin/update-status/${id}`;
    const body = {
      statusEn: statusEn,
      statusAr: statusAr
    };
    return this.http.put(url, body); 
  }







  getProducts(page: number = 1, pageSize: number = 10) {
    return this.http.get<PagedProductResponse>(`${this.productApiUrl}?page=${page}&pageSize=${pageSize}`);
  }

  addProduct(product: ProductCreateDto): Observable<any> {
    return this.http.post(`${this.AddProductApiUrl}`, product);
  }



  updateProductStatus(id: number, statusEn: string, statusAr: string): Observable<any> {
    const url = `https://localhost:7152/api/Admin/update-product-status/${id}`;
    const body = { statusEn, statusAr };
    return this.http.put(url, body);
  }





  translateText(text: string): Observable<string> {
    const body = {
      q: text,
      source: 'en',
      target: 'ar',
      format: 'text'
    };
    return this.http.post<any>(this.translateUrl, body).pipe(
      map(response => {
        console.log("📥 Response from translation API:", response);
        return response.translatedText;
      })
    );

  }



  getOrders(page: number = 1, pageSize: number = 10) {
    return this.http.get<pagedOrderRespose>(`${this.orderApiUrl}?page=${page}&pageSize=${pageSize}`);
  }



  createOrder(orderPayload: any): Observable<any> {
    return this.http.post('https://localhost:7086/api/Admin/orders', orderPayload);
  }

}
