import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { userLogin } from '../userlogin';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: userLogin = {
    userName: null,
    password: null,
    token: null
  }

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
  }

  login() {
    var url = environment.apibaseurl + '/api/user/login'
    this.httpClient.post<userLogin>(url,this.user)
    .subscribe( 
      (data) => this.user.token = data.token
    );
  }
}
