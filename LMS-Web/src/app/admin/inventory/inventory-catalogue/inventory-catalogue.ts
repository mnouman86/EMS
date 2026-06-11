import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TabsModule } from 'primeng/tabs';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { TextareaModule } from 'primeng/textarea';
import { SelectModule } from 'primeng/select';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { InventoryService } from '../inventory.service';
import { InventoryCategory, InventoryItem } from '../inventory.models';
import { defaultSearch } from '../../../core/models/search-request';
import { ToastService } from '../../../core/services/toast.service';
import { AuthService } from '../../../core/services/auth.service';
import { Roles } from '../../../core/models/roles';

@Component({
  selector: 'app-inventory-catalogue',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TabsModule, TableModule, ButtonModule, InputTextModule, InputNumberModule, TextareaModule,
    SelectModule, ToggleSwitchModule, DialogModule, TagModule, TooltipModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './inventory-catalogue.html'
})
export class InventoryCatalogue implements OnInit {
  private svc = inject(InventoryService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);
  private auth = inject(AuthService);

  canDelete = this.auth.hasAnyRole([Roles.Admin]);
  uomOptions = ['Piece', 'Box', 'Pack', 'Ream', 'Dozen', 'Set', 'Litre', 'Kg', 'Metre'].map(v => ({ label: v, value: v }));

  /* categories */
  categories = signal<InventoryCategory[]>([]);
  catLoading = signal(false);
  catDialog = signal(false);
  catSaving = signal(false);
  editCat: { id?: number | null; name: string; isActive: boolean } = { id: null, name: '', isActive: true };

  /* items */
  items = signal<InventoryItem[]>([]);
  itemsLoading = signal(false);
  itemDialog = signal(false);
  itemSaving = signal(false);
  categoryOptions = signal<{ label: string; value: number }[]>([]);
  editItem: { id?: number | null; name: string; code: string; categoryId: number | null; unitOfMeasure: string; description: string; reorderLevel: number; isActive: boolean } =
    { id: null, name: '', code: '', categoryId: null, unitOfMeasure: 'Piece', description: '', reorderLevel: 0, isActive: true };

  ngOnInit(): void {
    this.loadCategories();
    this.loadItems();
  }

  loadCategories(): void {
    this.catLoading.set(true);
    this.svc.getCategories().subscribe({
      next: res => {
        const cats = res.data ?? [];
        this.categories.set(cats);
        this.categoryOptions.set(cats.filter(c => c.isActive).map(c => ({ label: c.name, value: c.id })));
        this.catLoading.set(false);
      },
      error: () => this.catLoading.set(false)
    });
  }

  loadItems(): void {
    this.itemsLoading.set(true);
    this.svc.getItems(defaultSearch()).subscribe({
      next: res => { this.items.set(res.data ?? []); this.itemsLoading.set(false); },
      error: () => this.itemsLoading.set(false)
    });
  }

  /* category dialog */
  openCat(c?: InventoryCategory): void {
    this.editCat = c ? { id: c.id, name: c.name, isActive: c.isActive } : { id: null, name: '', isActive: true };
    this.catDialog.set(true);
  }
  saveCat(): void {
    if (!this.editCat.name.trim()) { this.toast.warn('Name is required.'); return; }
    this.catSaving.set(true);
    this.svc.upsertCategory(this.editCat).subscribe({
      next: () => { this.toast.success('Category saved'); this.catSaving.set(false); this.catDialog.set(false); this.loadCategories(); },
      error: () => this.catSaving.set(false)
    });
  }

  /* item dialog */
  openItem(i?: InventoryItem): void {
    this.editItem = i
      ? { id: i.id, name: i.name, code: i.code, categoryId: i.categoryId, unitOfMeasure: i.unitOfMeasure, description: i.description, reorderLevel: i.reorderLevel, isActive: i.isActive }
      : { id: null, name: '', code: '', categoryId: null, unitOfMeasure: 'Piece', description: '', reorderLevel: 0, isActive: true };
    this.itemDialog.set(true);
  }
  saveItem(): void {
    if (!this.editItem.name.trim()) { this.toast.warn('Name is required.'); return; }
    if (!this.editItem.categoryId) { this.toast.warn('Pick a category.'); return; }
    if (!this.editItem.unitOfMeasure) { this.toast.warn('Unit of measure is required.'); return; }
    this.itemSaving.set(true);
    this.svc.upsertItem({
      id: this.editItem.id,
      name: this.editItem.name,
      code: this.editItem.code || null,
      categoryId: this.editItem.categoryId,
      unitOfMeasure: this.editItem.unitOfMeasure,
      description: this.editItem.description || null,
      reorderLevel: this.editItem.reorderLevel,
      isActive: this.editItem.isActive
    }).subscribe({
      next: () => { this.toast.success('Item saved'); this.itemSaving.set(false); this.itemDialog.set(false); this.loadItems(); },
      error: () => this.itemSaving.set(false)
    });
  }
  deleteItem(i: InventoryItem): void {
    this.confirm.confirm({
      header: 'Delete item',
      message: `Delete ${i.name}? Items with movement history are soft-deleted.`,
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => this.svc.deleteItem(i.id).subscribe({ next: () => { this.toast.success('Item deleted'); this.loadItems(); } })
    });
  }
}
