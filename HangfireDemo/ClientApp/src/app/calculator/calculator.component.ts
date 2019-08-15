import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent implements OnInit {

  input1: number = 5;
  input2: number = 55;
  baseUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
  }

  add() {
    this.http.get<number>(this.baseUrl + `api/calculator/addnumbers?a=${this.input1}&b=${this.input2}`)
      .subscribe(data => alert(data),
        (err) => console.log(err));
    //TODO: Perform operation at server
    //alert(this.input1 + this.input2);
  }


}
