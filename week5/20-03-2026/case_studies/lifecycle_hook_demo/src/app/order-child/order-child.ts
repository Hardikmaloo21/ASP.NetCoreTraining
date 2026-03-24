import { Component, input , OnChanges , OnInit , DoCheck , AfterContentInit , AfterContentChecked , AfterViewInit , AfterViewChecked , OnDestroy , SimpleChanges, Input
} from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order-child',
  imports: [CommonModule],
  templateUrl: './order-child.html',
  styleUrl: './order-child.css',
})
export class OrderChild implements
 OnChanges,
 OnInit,
 DoCheck,
 AfterContentInit,
 AfterContentChecked,
 AfterViewInit,
 AfterViewChecked,
 OnDestroy {
  
  @Input() orderData : any;
  logs : string[] = [];
  log(message : string) {
    this.logs.push(`$(new Date().toLocaleTimeString()) - ${message}`);
  }

  ngOnChanges(changes: SimpleChanges){
    this.log('ngOnChanges - input data changed');
  }

  ngOnInit() {
    this.log('ngOnInit - component initialized');
  }

  ngDoCheck() {
    this.log('ngDoCheck - change detection');
  }

  ngAfterContentInit() {
    this.log('ngAfterContentInit - content initialized');
  }

  ngAfterContentChecked() {
    this.log('ngAfterContentChecked - content checked');
  }  
  ngOnDestroy() {
    this.log('ngOnDestroy - component destroyed');
  }
  ngAfterViewChecked(){
    this.log('ngAfterViewChecked - view checked');
  }
  ngAfterViewInit() {
    this.log('ngAfterViewInit - view initialized');
  }
}
