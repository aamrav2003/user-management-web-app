using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IAB_251_Assessment2.Application.Interfaces;
using IAB_251_Assessment2.BusinessLogic.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace IAB_251_Assessment2.Pages
{
    [Authorize(Roles = "Employee")]
    public class PrepareModel : PageModel
    {
        private readonly IQuoteAppService _quoteAppService;
        private readonly ILogger<PrepareModel> _logger;

        public PrepareModel(IQuoteAppService quoteAppService, ILogger<PrepareModel> logger)
        {
            _quoteAppService = quoteAppService;
            _logger = logger;
        }

        [BindProperty]
        public Quotation Quote { get; set; }
        
        public bool Calculated { get; set; } = false;
        
        public string ErrorMessage { get; set; }

        public IActionResult OnGet(int id)
        {
            try
            {
                var quote = _quoteAppService.GetQuote(id);
                if (quote == null) 
                {
                    _logger.LogWarning("Quote with ID {QuoteId} not found", id);
                    return RedirectToPage("/Quotation");
                }
                
                // Only allow preparation of quotes that are in "New" status
                if (quote.status != "New" && quote.status != "Prepared")
                {
                    _logger.LogWarning("Attempt to prepare quote {QuoteId} with invalid status: {Status}", id, quote.status);
                    return RedirectToPage("/Quotation");
                }
                
                Quote = quote;
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quote {QuoteId}", id);
                ErrorMessage = "An error occurred while loading the quotation. Please try again.";
                return RedirectToPage("/Quotation");
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) 
            {
                return Page();
            }

            try
            {
                // Validate business rules
                if (Quote.Rate <= 0)
                {
                    ModelState.AddModelError("Quote.Rate", "Rate must be greater than 0");
                    return Page();
                }

                if (Quote.Discount < 0 || Quote.Discount > 100)
                {
                    ModelState.AddModelError("Quote.Discount", "Discount must be between 0 and 100");
                    return Page();
                }

                // Calculate total price
                Quote.TotalPrice = Quote.Rate - (Quote.Rate * Quote.Discount / 100);
                
                // Update status and tracking fields
                Quote.status = "Prepared";
                Quote.PreparedBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown Employee";
                
                // Update the quotation
                _quoteAppService.UpdateQuote(Quote);
                
                _logger.LogInformation("Quote {QuoteId} prepared successfully by {Employee}", Quote.Id, Quote.PreparedBy);
                
                Calculated = true;
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error preparing quote {QuoteId}", Quote?.Id);
                ErrorMessage = "An error occurred while preparing the quotation. Please try again.";
                ModelState.AddModelError(string.Empty, "An error occurred while preparing the quotation. Please try again.");
                return Page();
            }
        }
    }
}
