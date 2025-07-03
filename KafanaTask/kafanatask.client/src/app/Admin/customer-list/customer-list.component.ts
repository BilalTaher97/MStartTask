import { Component, OnInit } from '@angular/core';
import { AdminServiceService, Customer } from '../../AdminService/admin-service.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {

  customers: Customer[] = [];                 // جميع العملاء المعروضين
  currentPage: number = 1;                    // رقم الصفحة الحالية

  searchId!: number;                          // رقم العميل المطلوب للبحث
  searchedCustomer: Customer | null = null;   // العميل الناتج من البحث
  searchError: string = '';                   // رسالة الخطأ عند عدم وجود العميل
  showDetails: boolean = false;               // لعرض التفاصيل الكاملة للعميل

  selectedCustomerIds: number[] = [];         // قائمة الـ IDs للعملاء المحددين للحذف
  selectAll: boolean = false;                 // حالة checkbox "تحديد الكل"

  constructor(private adminService: AdminServiceService) { }

  ngOnInit(): void {
    this.loadCustomers();                     // تحميل العملاء عند تحميل الصفحة
  }

  
  loadCustomers(): void {
    this.adminService.getCustomers(this.currentPage, 10).subscribe(response => {
      this.customers = response.data;
    });
  }

  /**
   * الذهاب للصفحة التالية
   */
  nextPage(): void {
    this.currentPage++;
    this.loadCustomers();
  }

  /**
   * الرجوع للصفحة السابقة
   */
  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadCustomers();
    }
  }

  /**
   * البحث عن عميل باستخدام الـ ID
   */
  searchCustomer(): void {
    this.adminService.getCustomerById(this.searchId).subscribe({
      next: (customer) => {
        this.searchedCustomer = customer;
        this.searchError = '';
      },
      error: () => {
        this.searchedCustomer = null;
        this.searchError = 'Customer not found';
      }
    });
  }

  /**
   * عرض أو إخفاء تفاصيل العميل
   */
  toggleDetails(): void {
    this.showDetails = !this.showDetails;
  }

  /**
   * عند الضغط على "تحديد الكل"
   */
  toggleAllSelection(): void {
    if (this.selectAll) {
      this.selectedCustomerIds = this.customers.map(c => c.id);
    } else {
      this.selectedCustomerIds = [];
    }
  }

  /**
   * عند تغيير تحديد أحد العملاء (checkbox)
   */
  onSelectionChange(): void {
    this.selectAll = this.selectedCustomerIds.length === this.customers.length;
  }


  toggleSelection(customerId: number, event: Event): void {
    const checked = (event.target as HTMLInputElement).checked;

    if (checked) {
      if (!this.selectedCustomerIds.includes(customerId)) {
        this.selectedCustomerIds.push(customerId);
      }
    } else {
      this.selectedCustomerIds = this.selectedCustomerIds.filter(id => id !== customerId);
    }

    this.selectAll = this.selectedCustomerIds.length === this.customers.length;
  }





  /**
   * حذف العملاء المحددين
   */
  deleteSelectedCustomers(): void {
    if (confirm('Are you sure you want to delete the selected customers?')) {
      this.adminService.deleteCustomers(this.selectedCustomerIds).subscribe({
        next: () => {
          this.customers = this.customers.filter(c => !this.selectedCustomerIds.includes(c.id));
          this.selectedCustomerIds = [];
          this.selectAll = false;
        },
        error: (err) => {
          alert('Error deleting customers.');
          console.error(err);
        }
      });
    }
  }


  deleteCustomer(customerId: number): void {
    if (confirm('Are you sure you want to delete this customer?')) {
      this.adminService.deleteCustomers([customerId]).subscribe({
        next: () => {
          this.customers = this.customers.filter(c => c.id !== customerId);
          this.selectedCustomerIds = this.selectedCustomerIds.filter(id => id !== customerId);
          this.selectAll = this.selectedCustomerIds.length === this.customers.length;
        },
        error: (err) => {
          alert('Error deleting customer.');
          console.error(err);
        }
      });
    }
  }


  translateStatusToArabic(statusEn: string): string {
    switch (statusEn) {
      case 'Active': return 'نشط';
      case 'Inactive': return 'غير نشط';
      default: return 'غير معروف';
    }
  }

  updateStatus(customer: Customer): void {
    const statusAr = this.translateStatusToArabic(customer.statusEn);

    this.adminService.updateCustomerStatus(customer.id, customer.statusEn, statusAr).subscribe({
      next: () => {
        console.log(`Updated status to ${customer.statusEn}/${statusAr} for customer ID ${customer.id}`);
      },
      error: err => {
        alert('Failed to update status');
        console.error(err);
      }
    });
  }




}
