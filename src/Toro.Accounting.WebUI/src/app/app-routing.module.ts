import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { AccountsComponent } from './accounts/accounts.component';

const routes: Routes = [
  { path: 'accounts/:userId', component: AccountDetailsComponent },
  { path: 'accounts', component: AccountsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
