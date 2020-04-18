import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { testModaliComponent } from './testModali.component';

describe('testModaliComponent', () => {
    let component: testModaliComponent;
    let fixture: ComponentFixture<testModaliComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
        declarations: [testModaliComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
      fixture = TestBed.createComponent(testModaliComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should display a title', async(() => {
    const titleText = fixture.nativeElement.querySelector('h1').textContent;
    expect(titleText).toEqual('Counter');
  }));

  it('should start with count 0, then increments by 1 when clicked', async(() => {
    const countElement = fixture.nativeElement.querySelector('strong');
    expect(countElement.textContent).toEqual('0');

    const incrementButton = fixture.nativeElement.querySelector('button');
    incrementButton.click();
    fixture.detectChanges();
    expect(countElement.textContent).toEqual('1');
  }));
});
