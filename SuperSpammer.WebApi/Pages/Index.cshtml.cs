using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperSpammer.Infastructure;

namespace SuperSpammer.WebApi.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, IAttendantService attendantService)
    {
        _logger = logger;
        _attendantService = attendantService;
        
    }

    [BindProperty]
    [Required]
    [EmailAddress]
    public string EmailAddressInputValue { get; set; }

    [BindProperty]
    [Required]
    public int? SelectedNumber { get; set; }

    public string Message { get; set; }

    public SelectList NumberOptions { get; set; }

    public void OnGet()
    {
        NumberOptions = new SelectList(Enumerable.Range(1, 5));
    }

    public void OnPost()
    {
        NumberOptions = new SelectList(Enumerable.Range(1, 5));

        if (SelectedNumber.HasValue && !string.IsNullOrWhiteSpace(EmailAddressInputValue))
        {
            _attendantService.ProcessMessages("lukas.srovnal.canada@gmail.com", EmailAddressInputValue, SelectedNumber.Value);
            Message = SelectedNumber.Value == 1 
                ? $"You sent 1 email on '{EmailAddressInputValue} email address." 
                : $"You sent {SelectedNumber} emails on '{EmailAddressInputValue} email address.";
        }
        else
        {
            Message = "Please enter a value and select a number.";
        }
    }
    
    readonly IAttendantService _attendantService;
}