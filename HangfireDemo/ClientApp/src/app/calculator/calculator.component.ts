import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from 'selenium-webdriver/http';
import { textDef } from '@angular/core/src/view';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent implements OnInit {

  input1: number;
  input2: number;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {   
  }

  ngOnInit() {
  }

  add(){
    //TODO: Perform operaiton at server
    alert(this.input1 + this.input2);
  }


}
