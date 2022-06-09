import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Accounting';

  RedirectToSwagger = function () {
    document.location.href = `${environment.baseUrl}/swagger/index.html`;
  }
}
