import { Component, OnInit } from '@angular/core';
import { HubConnection,HubConnectionBuilder } from '@aspnet/signalr';
import { build$ } from 'protractor/built/element';

@Component({
  selector: 'app-signalr-demo',
  templateUrl: './signalr-demo.component.html',
  styleUrls: ['./signalr-demo.component.css']
})
export class SignalrDemoComponent implements OnInit {

  private _hubConnection: HubConnection;
  msgs: any[] = [];

  constructor() { }

  ngOnInit(): void {
    this._hubConnection = new HubConnectionBuilder().withUrl('/notify').build();
    this._hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this._hubConnection.on('BroadcastMessage', (type: string, payload: string) => {
      this.msgs.push({ severity: type, summary: payload });
      console.log(payload);
    });
  }

}
