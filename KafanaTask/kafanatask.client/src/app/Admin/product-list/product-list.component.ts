import { Component, OnInit } from '@angular/core';
import { AdminServiceService, Product } from '../../AdminService/admin-service.service';
import { ToastrService } from 'ngx-toastr'; 


@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']  
})
export class ProductListComponent implements OnInit {

  products: Product[] = [];
  currentPage = 1;

  ngOnInit() {
    this.loadProducts();
  }

  constructor(private adminService: AdminServiceService, private toastr: ToastrService) { }

  loadProducts() {
    this.adminService.getProducts(this.currentPage, 10).subscribe(response => {
      this.products = response.data;
    })
  }



  nextPage(): void {
    this.currentPage++;
    this.loadProducts();
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadProducts();
    }
  }


  translateStatusToArabic(statusEn: string): string {
    switch (statusEn) {
      case 'Active': return 'نشط';
      case 'Inactive': return 'غير نشط';
      default: return 'غير معروف';
    }
  }

  updateStatus(product: Product): void {
    const statusAr = this.translateStatusToArabic(product.statusEn);

    this.adminService.updateProductStatus(product.id, product.statusEn, statusAr).subscribe({
      next: () => {
        console.log(`Product ID ${product.id} status updated`);
      },
      error: err => {
        alert('Failed to update product status');
        console.error(err);
      }
    });
  }



  createOrder(product: Product) {
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    const customerId = user?.id; 

    const orderPayload = {
      customerId,
      productId: product.id,
      totalAmount: product.amount,
      currency: product.currency
    };

    this.adminService.createOrder(orderPayload).subscribe({
      next: (res) => {
        this.toastr.success('Order created successfully!', 'Success');
        this.loadProducts();
      },
      error: (err) => {
        this.toastr.error('Failed to create order', 'Error');
      }
    });
  }



}
