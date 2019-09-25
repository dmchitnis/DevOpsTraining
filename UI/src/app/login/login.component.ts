import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user = 'Deepak';

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
  }

  login() {
    this.httpClient.get('https://localhost:5001/api/Values/1',)
    .subscribe( 
      (data) => this.user = data.Value
    );
  }
}
