import { Component, OnInit } from '@angular/core';
import { IAccountDetails } from '../shared/interfaces/account_details';
import { AccountDetailsService } from '../shared/services/account-details.service';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css']
})
export class AccountsComponent implements OnInit {

  accounts: IAccountDetails[] = [];

  constructor(private accountDetailsService: AccountDetailsService) { }

  ngOnInit(): void {

    this.accountDetailsService.getAccounts().subscribe((accounts) => {
      this.accounts = accounts;
    });
  }
}
