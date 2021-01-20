import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {FormBuilder} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {RegisterModel} from '../pages/register/register.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  // UserID:any
  // UserFullName : any
  // UserN : any
  constructor(private http: HttpClient) { }

  registerUser(registerUser: RegisterModel): Observable<any> {
    return this.http.post(environment.apiBaseUrl + '/account/Register', registerUser);
  }

  login(loginData: {Email: string}): Observable<any> {
    return this.http.post(environment.apiBaseUrl  + '/account/Login', loginData)
  }


  getUserProfile(): Observable<any>  {
    return this.http.get(environment.apiBaseUrl + '/UserProfile');
  }
  getAll(): Observable<any>  {
    return this.http.get(environment.apiBaseUrl  + '/account');
  }

}
