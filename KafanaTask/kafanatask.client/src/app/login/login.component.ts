import { Component } from '@angular/core';
import { AllServicesService } from '../Service/all-services.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']  
})
export class LoginComponent {

  constructor(private service: AllServicesService, private router: Router) { }


  language: 'en' | 'ar' = 'en';

 
  loginModel = {
    email: '',
    password: '',
  };

  
  labels = {
    en: {
      title: 'Login',
      email: 'Email',
      emailError: 'Please enter a valid email',
      password: 'Password',
      passwordError: 'Password must be at least 6 characters',
      submit: 'Login',
      toggle: 'عربي',
      registerLink: "Don't have an account? Register here"
    },
    ar: {
      title: 'تسجيل الدخول',
      email: 'البريد الإلكتروني',
      emailError: 'الرجاء إدخال بريد إلكتروني صحيح',
      password: 'كلمة المرور',
      passwordError: 'كلمة المرور يجب أن تكون 6 أحرف على الأقل',
      submit: 'دخول',
      toggle: 'English',
      registerLink: "لا تملك حساب؟ سجل هنا"
    },
  };

  
  toggleLanguage() {
    this.language = this.language === 'en' ? 'ar' : 'en';
  }

  


  onSubmit() {
    if (this.loginModel.email && this.loginModel.password) {
      this.service.login(this.loginModel.email, this.loginModel.password)
        .subscribe({
          next: (token) => {
            console.log('Login successful, token:', token);
       
            this.router.navigate(['/products']);
          },
          error: (err) => {
            console.error('Login failed', err);
            alert('فشل تسجيل الدخول: تأكد من البريد وكلمة المرور');
          }
        });
    } else {
      alert('الرجاء ملء جميع الحقول');
    }
  }
}
