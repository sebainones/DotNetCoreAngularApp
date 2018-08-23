using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetCoreAngularApp.Pages
{
    public class ContactModel : PageModel
    {
        // [AllowAnonymous]
        [Authorize]
        public void OnGet()
        {

        }
    }
}