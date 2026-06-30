import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { SearchRequest, defaultSearch } from '../../core/models/search-request';
import { ApiResult } from '../../core/models/api-result';
import {
  InventoryCategory, UpsertInventoryCategory, InventoryItem, UpsertInventoryItem,
  RecordPurchase, IssueItems, RecordReturn, StockAdjustment, IssueDetailRow,
  StockDashboardRow, LowStockAlert, SnoozeAlert,
  PurchaseRegisterRow, IssueRegisterRow, ItemLedgerRow, PurchaseDetailRow,
  PurchaseRegisterFilters, IssueRegisterFilters,
  MyIssuedRow, CreateIssueRequest, IssueRequestRow, IssueRequestLineRow
} from './inventory.models';

@Injectable({ providedIn: 'root' })
export class InventoryService {
  private api = inject(ApiService);

  /* INV-01: catalogue */
  getCategories(): Observable<ApiResult<InventoryCategory[]>> {
    return this.api.post<InventoryCategory[]>('Inventory/InventoryGetCategories', {});
  }
  upsertCategory(dto: UpsertInventoryCategory): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventoryUpsertCategory', dto);
  }
  getItems(search: SearchRequest = defaultSearch()): Observable<ApiResult<InventoryItem[]>> {
    return this.api.post<InventoryItem[]>('Inventory/InventoryGetItems', { searchRequest: search });
  }
  getItemById(id: number): Observable<ApiResult<InventoryItem>> {
    return this.api.post<InventoryItem>('Inventory/InventoryGetItemById', { id });
  }
  upsertItem(dto: UpsertInventoryItem): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventoryUpsertItem', dto);
  }
  deleteItem(id: number, forceHard = false): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventoryDeleteItem', { deleteRequest: { selectedIds: String(id), isDeleted: true, forceHard } });
  }

  /* INV-02 / 03 / 04 / 07: movements */
  recordPurchase(dto: RecordPurchase): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventoryRecordPurchase', dto);
  }
  issueItems(dto: IssueItems): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventoryIssueItems', dto);
  }
  cancelPurchase(purchaseId: number, reason: string): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventoryCancelPurchase', { purchaseId, reason });
  }
  cancelIssue(issueId: number, reason: string): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventoryCancelIssue', { issueId, reason });
  }
  recordReturn(dto: RecordReturn): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventoryRecordReturn', dto);
  }
  getIssueDetail(issueId: number): Observable<ApiResult<IssueDetailRow[]>> {
    return this.api.post<IssueDetailRow[]>('Inventory/InventoryGetIssueDetail', { issueId });
  }
  getPurchaseDetail(purchaseId: number): Observable<ApiResult<PurchaseDetailRow[]>> {
    return this.api.post<PurchaseDetailRow[]>('Inventory/InventoryGetPurchaseDetail', { purchaseId });
  }
  recordAdjustment(dto: StockAdjustment): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventoryRecordAdjustment', dto);
  }

  /* INV-05 / 06: dashboard + alerts */
  getStockDashboard(categoryId: number | null, status: string | null, fromDate: string, toDate: string): Observable<ApiResult<StockDashboardRow[]>> {
    return this.api.post<StockDashboardRow[]>('Inventory/InventoryGetStockDashboard', { categoryId, status, fromDate, toDate });
  }
  getLowStockAlerts(): Observable<ApiResult<LowStockAlert[]>> {
    return this.api.post<LowStockAlert[]>('Inventory/InventoryGetLowStockAlerts', {});
  }
  snoozeAlert(dto: SnoozeAlert): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventorySnoozeAlert', dto);
  }

  /* INV-08: reports (extended filters) */
  getPurchaseRegister(filters: PurchaseRegisterFilters): Observable<ApiResult<PurchaseRegisterRow[]>> {
    return this.api.post<PurchaseRegisterRow[]>('Inventory/InventoryGetPurchaseRegister', filters);
  }
  getIssueRegister(filters: IssueRegisterFilters): Observable<ApiResult<IssueRegisterRow[]>> {
    return this.api.post<IssueRegisterRow[]>('Inventory/InventoryGetIssueRegister', filters);
  }
  getItemLedger(itemId: number, fromDate: string, toDate: string): Observable<ApiResult<ItemLedgerRow[]>> {
    return this.api.post<ItemLedgerRow[]>('Inventory/InventoryGetItemLedger', { itemId, fromDate, toDate });
  }

  /* "My Issued Items" — staff self-service */
  getMyIssued(fromDate: string, toDate: string): Observable<ApiResult<MyIssuedRow[]>> {
    return this.api.post<MyIssuedRow[]>('Inventory/InventoryGetMyIssued', { fromDate, toDate });
  }

  /* Issue request workflow */
  createIssueRequest(dto: CreateIssueRequest): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventoryCreateIssueRequest', dto);
  }
  getIssueRequests(mineOnly: boolean, status: string | null, fromDate: string | null, toDate: string | null): Observable<ApiResult<IssueRequestRow[]>> {
    return this.api.post<IssueRequestRow[]>('Inventory/InventoryGetIssueRequests', { mineOnly, status, fromDate, toDate });
  }
  getIssueRequestLines(requestId: number): Observable<ApiResult<IssueRequestLineRow[]>> {
    return this.api.post<IssueRequestLineRow[]>('Inventory/InventoryGetIssueRequestLines', { requestId });
  }
  approveIssueRequest(requestId: number): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventoryApproveIssueRequest', { requestId });
  }
  rejectIssueRequest(requestId: number, reason: string): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventoryRejectIssueRequest', { requestId, reason });
  }
  fulfillIssueRequest(requestId: number, issueDate: string | null = null): Observable<ApiResult<unknown>> {
    return this.api.post('Inventory/InventoryFulfillIssueRequest', { requestId, issueDate });
  }
}
