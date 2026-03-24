import { Directive, ElementRef, Input, OnInit } from '@angular/core';

@Directive({
  selector: '[appPriceHighlight]'
})
export class PriceHighlightDirective implements OnInit {

  @Input() appPriceHighlight!: number;

  constructor(private el: ElementRef) {}

  ngOnInit() {
    if (this.appPriceHighlight > 50000) {
      this.el.nativeElement.style.backgroundColor = 'red';
      this.el.nativeElement.style.color = 'white';
    } else {
      this.el.nativeElement.style.backgroundColor = 'green';
      this.el.nativeElement.style.color = 'white';
    }
  }
}