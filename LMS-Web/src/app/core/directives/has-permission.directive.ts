import { Directive, Input, TemplateRef, ViewContainerRef, effect, inject } from '@angular/core';
import { PermissionService } from '../services/permission.service';

/**
 * Structural directive: renders its host element only when the current user holds
 * the given permission. Usage:
 *   <button *hasPermission="'Students.Create'">Add</button>
 *   <a *hasPermission="'Fees'">Fees</a>   // bare feature implies Read
 * Re-evaluates automatically when permissions load/change.
 */
@Directive({
  selector: '[hasPermission]',
  standalone: true
})
export class HasPermissionDirective {
  private tpl = inject(TemplateRef<unknown>);
  private vcr = inject(ViewContainerRef);
  private perms = inject(PermissionService);

  private spec = '';
  private shown = false;

  constructor() {
    // React to permission state changes (load, super-admin, assignment updates).
    effect(() => {
      this.perms.version(); // dependency
      this.update();
    });
  }

  @Input() set hasPermission(spec: string) {
    this.spec = spec || '';
    this.update();
  }

  private update(): void {
    const allowed = this.spec ? this.perms.has(this.spec) : true;
    if (allowed && !this.shown) {
      this.vcr.createEmbeddedView(this.tpl);
      this.shown = true;
    } else if (!allowed && this.shown) {
      this.vcr.clear();
      this.shown = false;
    }
  }
}
