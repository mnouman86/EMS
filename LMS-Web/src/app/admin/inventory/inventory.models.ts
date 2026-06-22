/* ---------- INV-01: Catalogue ---------- */
export interface InventoryCategory {
  id: number;
  name: string;
  isActive: boolean;
}

export interface UpsertInventoryCategory {
  id?: number | null;
  name: string;
  isActive: boolean;
}

export interface InventoryItem {
  id: number;
  name: string;
  code: string;
  categoryId: number;
  categoryName: string;
  unitOfMeasure: string;
  description: string;
  reorderLevel: number;
  currentStock: number;
  isActive: boolean;
}

export interface UpsertInventoryItem {
  id?: number | null;
  name: string;
  code?: string | null;
  categoryId: number;
  unitOfMeasure: string;
  description?: string | null;
  reorderLevel: number;
  isActive: boolean;
}

/* ---------- INV-02: Purchase ---------- */
export interface PurchaseLine {
  itemId: number;
  quantity: number;
  unitPrice: number;
}

export interface RecordPurchase {
  purchaseDate: string;
  vendorName: string;
  vendorInvoiceNo?: string | null;
  paymentMode?: string | null;
  notes?: string | null;
  attachmentPath?: string | null;
  lines: PurchaseLine[];
}

/* ---------- INV-03: Issue ---------- */
export interface IssueLine {
  itemId: number;
  quantity: number;
}

export interface IssueItems {
  issueDate: string;
  issuedToType: string; // Class / Teacher / Department / Student
  issuedToId?: number | null;
  issuedToName: string;
  purpose?: string | null;
  lines: IssueLine[];
}

/* ---------- INV-04: Return ---------- */
export interface IssueDetailRow {
  issueId: number;
  issueCode: string;
  issueDate: string;
  issuedToType: string;
  issuedToName: string;
  purpose: string;
  issueLineId: number;
  itemId: number;
  itemName: string;
  unitOfMeasure: string;
  quantity: number;
  returnedQuantity: number;
  remainingQuantity: number;
}

export interface ReturnLine {
  issueLineId: number;
  itemId: number;
  quantity: number;
  condition: string; // Good / Damaged / Lost
  notes?: string | null;
}

export interface RecordReturn {
  issueId: number;
  returnDate: string;
  notes?: string | null;
  lines: ReturnLine[];
}

/* ---------- INV-07: Adjustment ---------- */
export interface StockAdjustment {
  itemId: number;
  adjustmentType: string; // Increase / Decrease
  quantity: number;
  reason: string;
  approvedByUserId?: number | null;
}

/* ---------- INV-05: Stock dashboard ---------- */
export interface StockDashboardRow {
  itemId: number;
  itemName: string;
  code: string;
  categoryName: string;
  unitOfMeasure: string;
  openingStock: number;
  purchased: number;
  issued: number;
  adjusted: number;
  returned: number;
  currentStock: number;
  reorderLevel: number;
  status: string;
}

/* ---------- INV-06: Alerts ---------- */
export interface LowStockAlert {
  itemId: number;
  itemName: string;
  code: string;
  categoryName: string;
  currentStock: number;
  reorderLevel: number;
  lastPurchasedAt?: string | null;
  snoozedUntil?: string | null;
}

export interface SnoozeAlert {
  itemId: number;
  snoozeForDays: number;
  reason: string;
}

/* ---------- INV-08: Reports ---------- */
export interface PurchaseRegisterRow {
  id: number;
  purchaseCode: string;
  purchaseDate: string;
  vendorName: string;
  vendorInvoiceNo: string;
  paymentMode: string;
  grandTotal: number;
  linkedExpenseId?: number | null;
}

export interface IssueRegisterRow {
  id: number;
  issueCode: string;
  issueDate: string;
  issuedToType: string;
  issuedToId?: number | null;
  issuedToName: string;
  purpose: string;
  issuedBy?: number | null;
}

export interface ItemLedgerRow {
  when: string;
  movementType: string;
  reference: string;
  quantity: number;
  runningStock: number;
  notes: string;
}

/* ---------- INV-08 filters (extended) ---------- */
export interface PurchaseRegisterFilters {
  fromDate: string;
  toDate: string;
  categoryId?: number | null;
  vendorName?: string | null;
}
export interface IssueRegisterFilters {
  fromDate: string;
  toDate: string;
  categoryId?: number | null;
  itemId?: number | null;
  issuedToType?: string | null;
  issuedToId?: number | null;
  issuedByUserId?: number | null;
}

/* ---------- "My Issued Items" ---------- */
export interface MyIssuedRow {
  issueId: number;
  issueCode: string;
  issueDate: string;
  purpose: string;
  issueLineId: number;
  itemId: number;
  itemName: string;
  code: string;
  categoryName: string;
  unitOfMeasure: string;
  quantity: number;
  returnedQuantity: number;
  remainingQuantity: number;
}

/* ---------- Issue request workflow ---------- */
export interface IssueRequestLineInput {
  itemId: number;
  quantity: number;
}
export interface CreateIssueRequest {
  purpose: string;
  lines: IssueRequestLineInput[];
}
export interface IssueRequestRow {
  id: number;
  requestCode: string;
  requestDate: string;
  requestedByUserId: number;
  requestedByName: string;
  purpose: string;
  status: 'Pending' | 'Approved' | 'Rejected' | 'Fulfilled';
  approvedByUserId?: number | null;
  approvedByName?: string | null;
  approvedAt?: string | null;
  rejectedReason?: string | null;
  fulfilledIssueId?: number | null;
  fulfilledIssueCode?: string | null;
  lineCount: number;
  totalQuantity: number;
}
export interface IssueRequestLineRow {
  id: number;
  requestId: number;
  itemId: number;
  itemName: string;
  code: string;
  unitOfMeasure: string;
  quantity: number;
  currentStock: number;
}
