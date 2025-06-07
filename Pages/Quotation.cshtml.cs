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

        public QuotationModel(IQuoteAppService quoteAppService)
        {
            _quoteAppService = quoteAppService;
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
            if (!ModelState.IsValid)
            {
                // Reload the list so the page can render correctly
                OnGet();
                return Page();
            }

            var now = DateTime.Now;

            var entity = new Quotation
            {
                clientName = Quote.clientName,
                dateIssued = now,
                status = "New",
                Rate = 0m,
                Discount = 0m,
                TotalPrice = 0m,
                CustomerDecision = string.Empty,
                Comments = string.Empty,
                PreparedBy = string.Empty
            };

            _quoteAppService.AddQuote(entity);

            return RedirectToPage("/Quotation");
        }
    }
}
