using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Designly.Client.Pages;

public class LogoutModel : PageModel
{
    public IActionResult OnPost()
    {
        // Clear the token from TempData
        TempData.Remove("JwtToken");
        TempData.Clear();
        
        // Redirect to the Login page
        return RedirectToPage("/Index");
    }
}
