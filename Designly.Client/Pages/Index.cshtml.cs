using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Designly.Client.Pages;

    public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public string? ErrorMessage { get; set; }
    public bool IsLoggedIn { get; set; } = false; // New property

    public class InputModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }

    public void OnGet()
    {
        // Check for redirect error messages
        if (TempData["ErrorMessage"] is string error)
        {
            ErrorMessage = error;
        }

        // Check if a token exists, but don't consume TempData for further use
        if (TempData.Peek("JwtToken") is string token && !string.IsNullOrEmpty(token))
        {
            IsLoggedIn = true;
            TempData.Keep("JwtToken"); // Ensure token is available for Users page if navigated directly
        }
        else
        {
            IsLoggedIn = false;
        }
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var client = _httpClientFactory.CreateClient("DesignlyAPI");
        
        var loginData = new
        {
            Username = Input.Username,
            Password = Input.Password
        };

        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync("/api/user/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<LoginResult>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (result?.Token != null)
                {
                    TempData["JwtToken"] = result.Token;
                    return RedirectToPage("/Users");
                }
                
                ErrorMessage = "Login failed: No token received.";
            }
            else
            {
                ErrorMessage = "Invalid login attempt.";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling login API");
            ErrorMessage = "An error occurred while communicating with the server.";
        }

        return Page();
    }

    private class LoginResult
    {
        public string? Token { get; set; }
    }
}
