import { Component, OnInit } from '@angular/core';
import { userLogin } from '../userLogin';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  userLogin: user = {
    userName: 'Deepak',
    password: 'Test'
  }
  constructor() { }

  ngOnInit() {
  }
  
}
