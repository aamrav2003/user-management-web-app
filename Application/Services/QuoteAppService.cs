using IAB_251_Assessment2.Application.Interfaces;
using IAB_251_Assessment2.BusinessLogic.Entities;
using IAB_251_Assessment2.BusinessLogic.Interfaces;

namespace IAB_251_Assessment2.Application.Services
{
    public class QuoteAppService: IQuoteAppService
    {
        private readonly IQuotationService _quoteService;

        public QuoteAppService(IQuotationService quoteService)
        {
            _quoteService = quoteService;
        }

        public List<Quotation> GetAllQuotes() => _quoteService.GetAll();

        public Quotation GetQuote(int id) => _quoteService.GetById(id);

        public void AddQuote(Quotation quote) => _quoteService.Add(quote);

        public void UpdateQuote(Quotation quote) => _quoteService.Update(quote);

        public void DeleteQuote(int id) => _quoteService.Delete(id);
    }
}
