import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IAccountDetails } from '../shared/interfaces/account_details';
import { IMakeDepositRequest } from '../shared/interfaces/make_deposit_request';
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
    id: "",
    userName: "",
    cpf: "",
    accountNumber: "",
    accountBalance: 0
  };

  makeDepositRequest: IMakeDepositRequest = {
    event: "TRANSFER",
    origin: {
      bank: "001",
      branch: "0001",
      cpf: "00000000000"
    },
    target: {
      bank: "352",
      branch: "0001",
      account: "000000"
    },
    amount: 0,
  };

  userId: string = "";
  showClipboardMessage: boolean = false;

  constructor(
    private accountService: AccountDetailsService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.userId = this.route.snapshot.params['userId'];

    this.accountService.getAccount(this.userId).subscribe((accountDetails) => {
      this.accountDetails = accountDetails;

      this.makeDepositRequest.origin.cpf = this.accountDetails.cpf;
      this.makeDepositRequest.target.account = this.accountDetails.accountNumber;
    })
  }
}
