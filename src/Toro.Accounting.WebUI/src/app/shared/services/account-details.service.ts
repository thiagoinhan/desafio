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

  getAccount(userId: string) {
    let url = `${environment.baseUrl}/accounts/${userId}`;
    return this.http.get<IAccountDetails>(url);
  }

  getAccounts() {
    let url = `${environment.baseUrl}/accounts/`;
    return this.http.get<IAccountDetails[]>(url);
  }
}
