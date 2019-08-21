import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SignalrDemoComponent } from './signalr-demo.component';

describe('SignalrDemoComponent', () => {
  let component: SignalrDemoComponent;
  let fixture: ComponentFixture<SignalrDemoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SignalrDemoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SignalrDemoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
