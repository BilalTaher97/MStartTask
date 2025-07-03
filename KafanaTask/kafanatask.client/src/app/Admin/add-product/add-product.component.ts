import { Component } from '@angular/core';
import { AdminServiceService, ProductCreateDto } from '../../AdminService/admin-service.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {

  product: ProductCreateDto = {
    nameEn: '',
    nameAr: '',
    descriptionEn: '',
    descriptionAr: '',
    statusEn: '',
    statusAr: '',
    amount: 0,
    currency: ''
  };

  successMessage = '';
  errorMessage = '';

  constructor(private adminService: AdminServiceService) { }

  // إرسال الطلب إلى الباك
  onSubmit() {
    this.adminService.addProduct(this.product).subscribe({
      next: () => {
        this.successMessage = 'Product added successfully!';
        this.errorMessage = '';
        this.resetForm();
      },
      error: (err) => {
        this.errorMessage = 'Failed to add product.';
        this.successMessage = '';
        console.error(err);
      }
    });
  }

  // ترجمة تلقائية لاسم المنتج
  onNameEnChange(): void {
    if (this.product.nameEn?.trim()) {
      this.adminService.translateText(this.product.nameEn).subscribe({
        next: (translated: string) => {
          this.product.nameAr = translated;
        },
        error: () => {
          console.error('Translation failed');
        }
      });
    }
  }

  // ترجمة تلقائية للحالة
  onStatusEnChange(): void {
    const statusMap: any = {
      'Active': 'نشط',
      'Not Active': 'غير نشط',
      'Deleted': 'محذوف',
      'Expired': 'منتهي'
    };
    this.product.statusAr = statusMap[this.product.statusEn] || '';
  }

  // إعادة تعيين النموذج بعد الإضافة
  resetForm() {
    this.product = {
      nameEn: '',
      nameAr: '',
      descriptionEn: '',
      descriptionAr: '',
      statusEn: '',
      statusAr: '',
      amount: 0,
      currency: ''
    };
  }
}
