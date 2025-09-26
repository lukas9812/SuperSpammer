using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SuperSpammer.WebApi.Pages;

public class SenderSetup : PageModel
{
    [BindProperty]
    [Required]
    [EmailAddress]
    public string SenderEmail { get; set; }
    
    [BindProperty]
    [Required]
    public string SenderEmailPassword { get; set; }
    
    public string Message { get; set; }

    public void OnGet()
    {
        
    }
}