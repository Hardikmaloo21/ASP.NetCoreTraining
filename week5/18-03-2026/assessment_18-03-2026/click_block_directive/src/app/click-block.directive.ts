import { Directive, Input, ElementRef, OnChanges } from '@angular/core';

@Directive({
  selector: '[appClickBlock]'
})
export class ClickBlockDirective implements OnChanges {

  @Input() appClickBlock!: boolean;

  constructor(private el: ElementRef) {}

  ngOnChanges() {
    if (!this.appClickBlock) {
      // ❌ block → disable button
      this.el.nativeElement.disabled = true;
      this.el.nativeElement.style.cursor = 'not-allowed';
      this.el.nativeElement.style.opacity = '0.5';
    } else {
      // ✅ allow → enable button
      this.el.nativeElement.disabled = false;
      this.el.nativeElement.style.cursor = 'pointer';
      this.el.nativeElement.style.opacity = '1';
    }
  }
}