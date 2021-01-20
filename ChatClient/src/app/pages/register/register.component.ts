import { Component, OnInit } from '@angular/core';
import {RegisterModel} from './register.model';
import {UserService} from '../../services/user.service';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerUser: RegisterModel;
  loginEmail = '';
  constructor(private service: UserService,   private router: Router, private toastr: ToastrService) {
    this.registerUser = new RegisterModel();
  }

  ngOnInit(): void {
  }

  onRegisterSubmit(): void {
    this.registerUser.UserName = this.registerUser.Email;
    this.service.registerUser(this.registerUser).subscribe(
      (res: any) => {
        if (res == 1) {
          this.toastr.success('Registration is completed', 'Success');
        } else {
          this.toastr.error('Email is already used','Failed');
        }
      },
      err => {
        console.log(err);
      }
    );
  }

  onLoginSubmit(): void{
    this.service.login({Email: this.loginEmail}).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('login-user', JSON.stringify(res.user));
        this.router.navigateByUrl('');
      },
      err => {
        if (err.status == 400){
          this.toastr.warning('Email is not valid, Please register first', 'Failed');
        }
        else{
          console.log(err);
        }
      }
    );
  }

}
