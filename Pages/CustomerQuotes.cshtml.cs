using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IAB_251_Assessment2.Application.Interfaces;
using IAB_251_Assessment2.BusinessLogic.Entities;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IAB_251_Assessment2.Pages
{
    [Authorize(Roles = "Customer")]
    public class CustomerQuotesModel : PageModel
    {
        private readonly IQuoteAppService _quoteAppService;
        private readonly ILogger<CustomerQuotesModel> _logger;

        public CustomerQuotesModel(IQuoteAppService quoteAppService, ILogger<CustomerQuotesModel> logger)
        {
            _quoteAppService = quoteAppService;
            _logger = logger;
        }

        public List<Quotation> Quotes { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            try
            {
                // Get all quotes that are relevant to customers (prepared, accepted, or rejected)
                Quotes = _quoteAppService.GetAllQuotes()
                    .Where(q => q.status == "Prepared" || q.status == "Accepted" || q.status == "Rejected")
                    .OrderByDescending(q => q.dateIssued)
                    .ToList();

                _logger.LogInformation("Customer {CustomerEmail} viewed {QuoteCount} quotations", 
                    User.FindFirst(ClaimTypes.Name)?.Value, Quotes.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading quotations for customer {CustomerEmail}", 
                    User.FindFirst(ClaimTypes.Name)?.Value);
                ErrorMessage = "An error occurred while loading your quotations. Please try again.";
                Quotes = new List<Quotation>();
            }
        }

        public IActionResult OnGetAccept(int id)
        {
            try
            {
                var quote = _quoteAppService.GetQuote(id);
                if (quote == null)
                {
                    _logger.LogWarning("Customer {CustomerEmail} attempted to accept non-existent quote {QuoteId}", 
                        User.FindFirst(ClaimTypes.Name)?.Value, id);
                    TempData["ErrorMessage"] = "The quotation was not found.";
                    return RedirectToPage();
                }

                if (quote.status != "Prepared")
                {
                    _logger.LogWarning("Customer {CustomerEmail} attempted to accept quote {QuoteId} with invalid status {Status}", 
                        User.FindFirst(ClaimTypes.Name)?.Value, id, quote.status);
                    TempData["ErrorMessage"] = "This quotation cannot be accepted as it is no longer in 'Prepared' status.";
                    return RedirectToPage();
                }

                // Update quote status and tracking information
                quote.status = "Accepted";
                quote.CustomerDecision = "Accepted";
                quote.CustomerResponseDate = DateTime.Now;
                
                _quoteAppService.UpdateQuote(quote);

                _logger.LogInformation("Customer {CustomerEmail} accepted quote {QuoteId} for client {ClientName}", 
                    User.FindFirst(ClaimTypes.Name)?.Value, id, quote.clientName);

                TempData["SuccessMessage"] = $"Quotation for {quote.clientName} has been successfully accepted!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accepting quote {QuoteId} by customer {CustomerEmail}", 
                    id, User.FindFirst(ClaimTypes.Name)?.Value);
                TempData["ErrorMessage"] = "An error occurred while accepting the quotation. Please try again.";
            }

            return RedirectToPage();
        }

        public IActionResult OnGetReject(int id)
        {
            try
            {
                var quote = _quoteAppService.GetQuote(id);
                if (quote == null)
                {
                    _logger.LogWarning("Customer {CustomerEmail} attempted to reject non-existent quote {QuoteId}", 
                        User.FindFirst(ClaimTypes.Name)?.Value, id);
                    TempData["ErrorMessage"] = "The quotation was not found.";
                    return RedirectToPage();
                }

                if (quote.status != "Prepared")
                {
                    _logger.LogWarning("Customer {CustomerEmail} attempted to reject quote {QuoteId} with invalid status {Status}", 
                        User.FindFirst(ClaimTypes.Name)?.Value, id, quote.status);
                    TempData["ErrorMessage"] = "This quotation cannot be rejected as it is no longer in 'Prepared' status.";
                    return RedirectToPage();
                }

                // Update quote status and tracking information
                quote.status = "Rejected";
                quote.CustomerDecision = "Rejected";
                quote.CustomerResponseDate = DateTime.Now;
                
                _quoteAppService.UpdateQuote(quote);

                _logger.LogInformation("Customer {CustomerEmail} rejected quote {QuoteId} for client {ClientName}", 
                    User.FindFirst(ClaimTypes.Name)?.Value, id, quote.clientName);

                TempData["SuccessMessage"] = $"Quotation for {quote.clientName} has been rejected.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error rejecting quote {QuoteId} by customer {CustomerEmail}", 
                    id, User.FindFirst(ClaimTypes.Name)?.Value);
                TempData["ErrorMessage"] = "An error occurred while rejecting the quotation. Please try again.";
            }

            return RedirectToPage();
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            // Handle TempData messages
            if (TempData.ContainsKey("SuccessMessage"))
            {
                SuccessMessage = TempData["SuccessMessage"]?.ToString();
            }
            if (TempData.ContainsKey("ErrorMessage"))
            {
                ErrorMessage = TempData["ErrorMessage"]?.ToString();
            }
            
            base.OnPageHandlerExecuting(context);
        }
    }
}
