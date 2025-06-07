using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IAB_251_Assessment2.Application.Interfaces;
using IAB_251_Assessment2.BusinessLogic.Entities;
using IAB_251_Assessment2.Pages.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace IAB_251_Assessment2.Pages
{
    [Authorize(Roles = "Employee,Customer")]
    public class QuotationModel : PageModel
    {
        private readonly IQuoteAppService _quoteAppService;
        private readonly ILogger<QuotationModel> _logger;

        public QuotationModel(IQuoteAppService quoteAppService, ILogger<QuotationModel> logger)
        {
            _quoteAppService = quoteAppService;
            _logger = logger;
            Quote = new QuoteViewModel();
            QuoteList = new List<QuoteViewModel>();
        }

        [BindProperty]
        public QuoteViewModel Quote { get; set; }

        public List<QuoteViewModel> QuoteList { get; set; }

        public void OnGet()
        {
            var quotes = _quoteAppService.GetAllQuotes();

            QuoteList = quotes.Select(q => new QuoteViewModel
            {
                Id = q.Id,
                clientName = q.clientName,
                dateIssued = q.dateIssued,
                status = q.status,
                Rate = q.Rate,
                Discount = q.Discount,
                TotalPrice = q.TotalPrice,
                CustomerDecision = q.CustomerDecision
            }).ToList();
        }

        public IActionResult OnPost()
        {
            _logger.LogInformation("OnPost started");
            _logger.LogInformation($"Quote object: {Quote?.clientName ?? "null"}");

            foreach (var modelStateEntry in ModelState.Values)
            {
                foreach (var error in modelStateEntry.Errors)
                {
                    _logger.LogWarning($"ModelState error: {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid");
                OnGet();
                return Page();
            }

            if (Quote == null)
            {
                _logger.LogError("Quote object is null");
                ModelState.AddModelError(string.Empty, "Quote data is missing");
                OnGet();
                return Page();
            }

            try
            {
                var now = DateTime.Now;
                _logger.LogInformation($"Creating new quotation for client: {Quote.clientName}");

                var entity = new Quotation
                {
                    clientName = Quote.clientName,
                    dateIssued = now,
                    status = "New",
                    Rate = 0.01m,
                    Discount = 0m,
                    TotalPrice = 0m,
                    CustomerDecision = string.Empty,
                    Comments = string.Empty,
                    PreparedBy = string.Empty
                };

                _logger.LogInformation("About to call _quoteAppService.AddQuote");
                _quoteAppService.AddQuote(entity);
                _logger.LogInformation($"Quotation created successfully with ID: {entity.Id}");

                TempData["SuccessMessage"] = "Quotation created successfully!";
                return RedirectToPage("/Quotation");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating quotation: {ex.Message}");
                _logger.LogError($"Stack trace: {ex.StackTrace}");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the quotation.");
                OnGet();
                return Page();
            }
        }
    }
}
