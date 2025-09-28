using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace SuperSpammer.WebApi.Pages;

public class SenderSetup : PageModel
{
    public SenderSetup(ILoggerFactory loggerFactory, IMemoryCache cache)
    {
        _logger = loggerFactory.CreateLogger<SenderSetup>();
        _cache = cache;
    }
    
    [BindProperty]
    [Required]
    [EmailAddress]
    public string SenderEmail { get; set; }
    
    [BindProperty]
    [Required]
    public string SenderEmailPassword { get; set; }
    
    public string Message { get; set; }

    public void OnPost()
    {
        _cache.Set("UserEmail", SenderEmail, TimeSpan.FromMinutes(15));
        _cache.Set("UserEmailPwd", SenderEmail, TimeSpan.FromMinutes(15));
        _logger.LogInformation("Credentials were set to the cache memory.");
    }
    public void OnGet()
    {
        SenderEmail = _cache.Get<string>("UserEmail") ?? "!NotFound!";
        SenderEmailPassword = _cache.Get<string>("UserEmailPwd") ?? "!NotFound!";
    }
    
    readonly IMemoryCache _cache;
    readonly ILogger<SenderSetup> _logger;
}