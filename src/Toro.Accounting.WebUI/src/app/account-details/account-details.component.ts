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
    id: "",
    userName: "",
    cpf: "",
    accountNumber: "",
    accountBalance: 0
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
    })
  }

  CopyTransferToClipboard = () => {
    var transferTemplate = `
    {
        "event": "TRANSFER",
        "target": {
            "bank": "352",
            "branch": "001",
            "account": "${this.accountDetails.accountNumber}"
        },
        "origin": {
            "bank": "001",
            "branch": "001",
            "cpf": "${this.accountDetails.cpf}"
        },
        "amount": 0
    }`

    navigator.clipboard.writeText(transferTemplate);

    this.showClipboardMessage = true;

    setTimeout(() => {
      this.showClipboardMessage = false;
    }, 3000)
  }
}
