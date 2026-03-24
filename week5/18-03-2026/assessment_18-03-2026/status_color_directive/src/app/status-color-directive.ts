import { Directive, ElementRef, Input, OnChanges } from '@angular/core';

@Directive({
  selector: '[appStatusColor]'
})
export class StatusColorDirective implements OnChanges {

  @Input() appStatusColor!: number;

  constructor(private el: ElementRef) {}

  ngOnChanges() {
    if (this.appStatusColor >= 50) {
      // ✅ PASS → Green
      this.el.nativeElement.style.color = 'green';
      this.el.nativeElement.style.fontWeight = 'bold';
    } else {
      // ❌ FAIL → Red
      this.el.nativeElement.style.color = 'red';
      this.el.nativeElement.style.fontWeight = 'bold';
    }
  }
}