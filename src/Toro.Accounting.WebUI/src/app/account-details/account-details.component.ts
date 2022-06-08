import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IAccountDetails } from '../shared/interfaces/account_details';
import { AccountDetailsService } from '../shared/services/account-details.service';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css']
})
export class AccountDetailsComponent implements OnInit {

  accountDetails: IAccountDetails = {
    bank: "",
    branch: "",
    userName: "",
    accountNumber: "",
    accountBalance: 0
  };

  userId: string = "";

  constructor(
    private accountService: AccountDetailsService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.userId = this.route.snapshot.params['userId'];

    this.accountService.getAccountDetails(this.userId).subscribe((accountDetails) => {
      this.accountDetails = accountDetails;
    })
  }
}
