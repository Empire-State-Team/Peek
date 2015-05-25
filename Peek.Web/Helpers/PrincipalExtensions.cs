namespace Peek.Web.Helpers
{
    using System.Security.Principal;
    using Microsoft.AspNet.Identity;
    using Peek.Common;

    public static class PrincipalExtensions
    {
        public static string GetUsername(this IPrincipal principal)
        {
            return principal.Identity.GetUserName();
        }

        public static bool IsLoggedIn(this IPrincipal principal)
        {
            return principal.Identity.IsAuthenticated;
        }

        public static bool IsAdmin(this IPrincipal principal)
        {
            return principal.IsInRole(PeekConstants.AdminRole);
        }
    }
}
