namespace AppEmpleo.Class.Utilities.Normalization
{
    public static class RoleConstants
    {
        public const string Candidate = "CANDIDATO";
        public const string Recruiter = "RECLUTADOR";

        public static string Normalize(string? role)
        {
            return string.IsNullOrWhiteSpace(role) ? string.Empty : role.Trim().ToUpperInvariant();
        }

        public static bool IsValid(string? role)
        {
            var normalized = Normalize(role);
            return normalized == Candidate || normalized == Recruiter;
        }
    }
}
