export type PermissionActionName = 'Read' | 'Create' | 'Update' | 'Delete' | 'Export' | 'Approve' | 'Print';

export interface EffectivePermission {
  featureCode: string;
  canRead: boolean;
  canCreate: boolean;
  canUpdate: boolean;
  canDelete: boolean;
  canExport: boolean;
  canApprove: boolean;
  canPrint: boolean;
}

export interface MyPermissions {
  isSuperAdmin: boolean;
  permissions: EffectivePermission[];
}

export interface AppFeature {
  featureId: number;
  featureName: string;
  featureCode: string;
  parentFeatureId?: number | null;
  route?: string | null;
  icon?: string | null;
  displayOrder: number;
  isActive: boolean;
}

export interface UserPermissionGridRow {
  featureId: number;
  featureName: string;
  featureCode: string;
  parentFeatureId?: number | null;
  route?: string | null;
  displayOrder: number;
  canRead: boolean;
  canCreate: boolean;
  canUpdate: boolean;
  canDelete: boolean;
  canExport: boolean;
  canApprove: boolean;
  canPrint: boolean;
}

export interface RolePermission {
  featureId: number;
  featureCode: string;
  canRead: boolean;
  canCreate: boolean;
  canUpdate: boolean;
  canDelete: boolean;
  canExport: boolean;
  canApprove: boolean;
  canPrint: boolean;
}

export interface PermissionUser {
  userId: number;
  userName: string;
  fullName: string;
  email: string;
  roleName: string;
  department?: string | null;
  status: string;
  lastLogin?: string | null;
}

export interface RoleLookup {
  roleId: number;
  roleName: string;
}

export interface FeaturePermissionInput {
  featureId: number;
  canRead: boolean;
  canCreate: boolean;
  canUpdate: boolean;
  canDelete: boolean;
  canExport: boolean;
  canApprove: boolean;
  canPrint: boolean;
}
