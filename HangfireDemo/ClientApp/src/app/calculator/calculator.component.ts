import { Component, OnInit, Inject } from '@angular/core';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent implements OnInit {

  input1: number;
  input2: number;

  constructor() {   
  }

  ngOnInit() {
  }

  add(){
    //TODO: Perform operaiton at server
    alert(this.input1 + this.input2);
  }


}
