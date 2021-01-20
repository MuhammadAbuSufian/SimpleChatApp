import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private http: HttpClient){  }

  getUserReceivedMessages(userId: string): Observable<any> {
    return this.http.get(environment.apiBaseUrl + '/message');
  }

}
