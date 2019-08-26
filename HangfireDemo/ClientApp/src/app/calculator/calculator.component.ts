import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { HubConnection,HubConnectionBuilder } from '@aspnet/signalr';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent implements OnInit {

  input1: number = 5;
  input2: number = 55;
  baseUrl: string;

  private _hubConnection: HubConnection;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private toastr: ToastrService) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this._hubConnection = new HubConnectionBuilder().withUrl('/notify').build();
    this._hubConnection
      .start()
      .then(() => console.log('Connection started! waiting for result...'))
      .catch(err => this.toastr.error('Error while establishing connection :');

      this._hubConnection.on('BroadcastMessage', (type: string, payload: string) => {
        this.toastr.success("Result of recent add Later is  " + payload.toString(),'Add Later,' {
          positionClass: 'toast-bottom-right' 
       } );
        console.log(type);
      });
  }

  add() {
    this.http.get<number>(this.baseUrl + `api/calculator/addnumbers?a=${this.input1}&b=${this.input2}`)
      .subscribe(data =>{
        this.toastr.success("Result of recent add  is  " + data.toString() );
      }, 
        (err) => this.toastr.error('Error',err));
  }

  addLater() {
    var request = { a: this.input1, b: this.input2 };
    this.http.post(this.baseUrl + `api/calculator/addlater`,request )
      .subscribe(data => console.log('data from add later' + data), //should get nothing anyway!!
        (err) => console.log(err));   
  }

  testToast(){
    this.toastr.info("toast title","toast message is working!");
  }

}
