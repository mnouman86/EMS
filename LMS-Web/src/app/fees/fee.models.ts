/* ---------- FEE-01: Types / structure / calendar ---------- */
export interface FeeType {
  id: number;
  name: string;
  code: string;
  category: string; // OneTime / Monthly / Annual / Periodic
  isActive: boolean;
}

export interface UpsertFeeType {
  id?: number | null;
  name: string;
  code: string;
  category: string;
  isActive: boolean;
}

export interface FeeStructureRow {
  feeTypeId: number;
  feeTypeName: string;
  feeTypeCode: string;
  category: string;
  schoolClassId: number;
  className: string;
  amount: number;
  effectiveFrom: string;
}

export interface UpsertFeeAmount {
  feeTypeId: number;
  schoolClassId: number;
  amount: number;
  effectiveFrom: string;
}

export interface ConfigureFeeCalendar {
  feeTypeId: number;
  academicYearId: number;
  billedMonths: number[];
}

/* ---------- FEE-02: Invoices ---------- */
export interface GenerateInvoices {
  academicYearId: number;
  billingMonth: number;
  billingYear: number;
  classId?: number | null;
  excludedStudentIds?: number[];
  dryRun: boolean;
}

export interface InvoicePreviewRow {
  studentId: number;
  studentCode?: string | null;
  studentFullName?: string | null;
  classId: number;
  className?: string | null;
  grossAmount: number;
  concessionAmount: number;
  netAmount: number;
  note?: string | null;
}

export interface CancelInvoice {
  invoiceId: number;
  reason: string;
}

/* ---------- FEE-03 / 08 / 11: Payments ---------- */
export interface PaymentAllocation {
  invoiceId: number;
  amount: number;
}

export interface RecordPayment {
  studentId: number;
  paymentDate: string;
  amount: number;
  paymentMode: string; // Cash / Cheque / BankTransfer / Online
  referenceNo?: string | null;
  allocations?: PaymentAllocation[];
  remarks?: string | null;
  collectingStaffId: number;
}

export interface ClearCheque {
  paymentId: number;
  clearanceDate: string;
  bounced: boolean;
}

export interface ApplyAdvance {
  studentId: number;
  invoiceId: number;
  amount: number;
}

export interface ReversePayment {
  paymentId: number;
  reason: string;
}

/* ---------- FEE-04 / 13: Ledger ---------- */
export interface StudentLedgerEntry {
  date: string;
  type: string;
  reference: string;
  description: string;
  debit: number;
  credit: number;
  runningBalance: number;
}

/* ---------- FEE-05: Dashboard ---------- */
export interface CollectionSummary {
  totalCollected: number;
  totalPending: number;
  totalInvoiced: number;
  collectionRatePercent: number;
}

export interface CollectionByClass {
  classId: number;
  className: string;
  studentCount: number;
  invoiced: number;
  collected: number;
  pending: number;
  ratePercent: number;
}

export interface CollectionByDay {
  day: string;
  amount: number;
}

/* ---------- FEE-06 / 07 / 14: Reports ---------- */
export interface PendingFeeRow {
  studentId: number;
  studentCode: string;
  studentFullName: string;
  classId: number;
  className: string;
  monthsDue: string;
  totalOutstanding: number;
  daysOverdue: number;
  parentMobile: string;
  emergencyContactPhone: string;
  hasActiveWaiver: boolean;
}

export interface AgeingRow {
  studentId: number;
  studentCode: string;
  studentFullName: string;
  bucket0_30: number;
  bucket31_60: number;
  bucket61_90: number;
  bucket91Plus: number;
  total: number;
}

/* ---------- FEE-08 / 09: Concessions / advance ---------- */
export interface AdvanceBalance {
  studentId: number;
  availableBalance: number;
  lastUpdatedAt: string;
}

export interface Concession {
  id: number;
  studentId: number;
  concessionType: string;
  value: number;
  applicableFeeTypesJson: string;
  effectiveFrom: string;
  effectiveTo?: string | null;
  reason: string;
  status: string;
}

export interface GrantConcession {
  studentId: number;
  concessionType: string; // Percentage / Fixed / Sibling / FullWaiver
  value: number;
  applicableFeeTypeIds?: number[];
  effectiveFrom: string;
  effectiveTo?: string | null;
  reason?: string | null;
  approvedByUserId?: number | null;
}

export interface RevokeConcession {
  concessionId: number;
  reason: string;
}

/* ---------- FEE-10: Reminders ---------- */
export interface SendReminders {
  studentIds?: number[];
  channel: string; // Whatsapp / Sms / Both
  templateKey?: string | null;
  customBody?: string | null;
}

export interface SendRemindersResult {
  targetCount: number;
  sentCount: number;
  failedCount: number;
}

/* ---------- FEE-12: Arrears ---------- */
export interface CarryForwardArrears {
  fromAcademicYearId: number;
  toAcademicYearId: number;
}

export interface WriteOffArrear {
  arrearId: number;
  reason: string;
  approvedByUserId?: number | null;
}

export interface FeeArrearRow {
  id: number;
  studentId: number;
  studentCode: string;
  studentFullName: string;
  fromAcademicYearId: number;
  toAcademicYearId: number;
  fromYearName: string;
  toYearName: string;
  amount: number;
  isWrittenOff: boolean;
  writeOffReason?: string | null;
  createdAt: string;
}

/* ---------- FEE-13: Parent self-service ---------- */
export interface ParentFeeSummary {
  studentId: number;
  studentCode: string;
  studentFullName: string;
  currentMonthStatus: string;
  totalOutstanding: number;
  lastPaymentDate?: string | null;
}

/* ---------- Class Fee Board (bulk collection) ---------- */
export interface FeeClassBoardRow {
  studentId: number;
  studentCode?: string | null;
  formNo?: string | null;
  fullName: string;
  parentName?: string | null;
  parentMobile?: string | null;
  totalInvoiced: number;
  totalPaid: number;
  outstanding: number;
  lastPaymentDate?: string | null;
  lastPaymentAmount?: number | null;
  monthsDue?: string | null;
  invoiceCount: number;
}
