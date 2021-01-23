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
        if (res != null) {
          this.toastr.success('Registration is completed', 'Success');
          this.router.navigateByUrl('login');
        } else {
          this.toastr.error('Email is already used','Failed');
        }
      },
      err => {
        console.log(err);
      }
    );
  }

}
