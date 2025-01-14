using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WoodyFrontToBack.Helpers.Emails;
using WoodyFrontToBack.Models;
using WoodyFrontToBack.Services.Interfaces;

namespace WoodyFrontToBack.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class ConfirmEmailModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMailService _mailService;

    public ConfirmEmailModel(UserManager<AppUser> userManager, IMailService mailService)
    {
        _userManager = userManager;
        _mailService = mailService;
    }

    [TempData]
    public string StatusMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return RedirectToPage("/Index");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);

        var mailRequest = new MailRequest
        {
            ToEmail = user.Email,
            Subject = "Email Confirmed Successfully",
            Body = $"Thank you for confirming your email. Your account is now active."
        };

        await _mailService.SendEmailAsync(mailRequest);

        StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
        return Page();
    }
}
