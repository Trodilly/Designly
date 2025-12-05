using System.Net.Http.Headers;
using System.Text.Json;
using Designly.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Designly.Client.Pages;

public class UsersModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UsersModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<UserDto> Users { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        // Retrieve the token from TempData
        if (TempData["JwtToken"] is not string token)
        {
            // If no token, redirect back to login
            return RedirectToPage("/Index");
        }

        // Keep the token for the next request if needed (TempData is one-read)
        TempData.Keep("JwtToken"); 

        var client = _httpClientFactory.CreateClient("DesignlyAPI");
        
        // Add Authorization Header
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await client.GetAsync("/api/user");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Users = JsonSerializer.Deserialize<List<UserDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
            else
            {
                ErrorMessage = $"Error fetching users: {response.StatusCode}";
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToPage("/Index");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Exception: {ex.Message}";
        }

        return Page();
    }
}
