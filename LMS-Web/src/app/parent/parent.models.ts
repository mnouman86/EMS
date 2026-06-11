/** A child linked to the signed-in parent (drives dashboard + child switcher). */
export interface ParentChild {
  studentId: number;
  studentCode?: string | null;
  formNo?: string | null;
  fullName: string;
  classId?: number | null;
  className?: string | null;
  status?: string | null;
  dateOfBirth?: string | null;
  relationship?: string | null;
}

/** A child's fee payment (receipt download list). */
export interface StudentPayment {
  paymentId: number;
  receiptNo?: string | null;
  paymentDate: string;
  totalAmount: number;
  paymentMode?: string | null;
  referenceNo?: string | null;
}
