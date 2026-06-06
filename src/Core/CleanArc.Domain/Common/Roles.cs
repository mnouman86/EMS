namespace CleanArc.Domain.Common
{
    /// <summary>
    /// Role names used across the EMS modules. Mirror these in
    /// <c>SeedDataBase.Seed()</c> so the identity store carries them.
    /// </summary>
    public static class Roles
    {
        // Values must mirror the role names seeded by SeedDataBase.Seed() (lowercase).
        // JWT role claims carry the literal Role.Name from Identity, so any mismatch
        // here breaks User.IsInRole(...) (ordinal case-sensitive comparison).
        public const string Admin = "admin";
        public const string Principal = "principal";
        public const string Accountant = "accountant";
        public const string Teacher = "teacher";
        public const string Parent = "parent";

        public const string AdminOrPrincipal = Admin + "," + Principal;
        public const string AdminOrAccountant = Admin + "," + Accountant;
        public const string FinanceRoles = Admin + "," + Principal + "," + Accountant;
    }
}
