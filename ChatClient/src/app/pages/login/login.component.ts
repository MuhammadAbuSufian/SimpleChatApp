import { Component, OnInit } from '@angular/core';
import {RegisterModel} from '../register/register.model';
import {UserService} from '../../services/user.service';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  registerUser: RegisterModel;
  loginEmail = '';
  constructor(private service: UserService,   private router: Router, private toastr: ToastrService) {
    this.registerUser = new RegisterModel();
  }

  ngOnInit(): void {
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
