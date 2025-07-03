import { Component, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AllServicesService } from '../Service/all-services.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  customerForm!: FormGroup;
  isArabic: boolean = true;
  photoPreview: string | ArrayBuffer | null = null;
  isLoading: boolean = false;

  @ViewChild('photoInput') photoInput!: ElementRef;

  constructor(private fb: FormBuilder,
    private customerService: AllServicesService) {
    this.createForm();
  }

  createForm() {
    this.customerForm = this.fb.group({
      nameAr: ['', Validators.required],
      nameEn: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern('[0-9]{10}')]],
      dateOfBirth: ['', Validators.required],
      gender: ['', Validators.required],
      password: ['', Validators.required],
      status: ['active'],
      photo: [null],
    });
  }

  toggleLanguage() {
    this.isArabic = !this.isArabic;
  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.customerForm.patchValue({ photo: file });

      const reader = new FileReader();
      reader.onload = () => this.photoPreview = reader.result;
      reader.readAsDataURL(file);
    }
  }

  onSubmit() {
    if (this.customerForm.valid) {
      this.isLoading = true;

      const formData = new FormData();
      formData.append('nameAr', this.customerForm.get('nameAr')?.value);
      formData.append('nameEn', this.customerForm.get('nameEn')?.value);
      formData.append('email', this.customerForm.get('email')?.value);
      formData.append('phone', this.customerForm.get('phone')?.value);
      formData.append('dateOfBirth', this.customerForm.get('dateOfBirth')?.value);
      formData.append('genderInput', this.customerForm.get('gender')?.value);
      formData.append('password', this.customerForm.get('password')?.value);

      if (this.customerForm.get('photo')?.value) {
        formData.append('photo', this.customerForm.get('photo')?.value);
      }

      this.customerService.registerCustomer(formData).subscribe(
        (response) => {
          this.isLoading = false;
          alert(this.isArabic ? 'تم التسجيل بنجاح!' : 'Registration successful!');
          this.customerForm.reset();
          this.photoPreview = null;

     
          this.photoInput.nativeElement.value = null;
        },
        (error) => {
          this.isLoading = false;
          alert(this.isArabic ? 'حدث خطأ أثناء التسجيل' : 'Registration failed');
          console.error('Error:', error);
        }
      );
    }
  }
}
