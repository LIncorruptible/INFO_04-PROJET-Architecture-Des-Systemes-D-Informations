using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROJET_ASI.Data;

namespace PROJET_ASI.Pages
{
    public class Base : PageModel
    {
        protected ApplicationDbContext Context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<IdentityUser> UserManager { get; }

        public Base
            (
                ApplicationDbContext context,
                IAuthorizationService authorizationService,
                UserManager<IdentityUser> userManager
            ) : base()
        {
            Context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        }
    }
}
