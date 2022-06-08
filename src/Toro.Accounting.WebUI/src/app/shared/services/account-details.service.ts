import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IAccountDetails } from '../interfaces/account_details';

@Injectable({
  providedIn: 'root'
})
export class AccountDetailsService {

  constructor(private http: HttpClient) { 

  }

  getAccountDetails(userId: string) {
    let url = `${environment.baseUrl}/account/details?userId=${userId}`;
    return this.http.get<IAccountDetails>(url);
  }
}
