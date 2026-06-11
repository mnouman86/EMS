/** Mirror of the backend CleanArc.Domain.Common.Roles constants. */
export const Roles = {
  Admin: 'admin',
  Principal: 'principal',
  Accountant: 'accountant',
  Teacher: 'teacher',
  Parent: 'parent'
} as const;

export type RoleName = (typeof Roles)[keyof typeof Roles];

/** Convenience composite sets used for route data `roles`. */
export const RoleSets = {
  AdminOnly: [Roles.Admin],
  AdminOrPrincipal: [Roles.Admin, Roles.Principal],
  AdminOrAccountant: [Roles.Admin, Roles.Accountant],
  Finance: [Roles.Admin, Roles.Principal, Roles.Accountant],
  AnyStaff: [Roles.Admin, Roles.Principal, Roles.Accountant, Roles.Teacher]
} as const;
