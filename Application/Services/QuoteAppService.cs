using IAB_251_Assessment2.Application.Interfaces;
using IAB_251_Assessment2.BusinessLogic.Entities;
using IAB_251_Assessment2.BusinessLogic.Interfaces;

namespace IAB_251_Assessment2.Application.Services
{
    public class QuoteAppService: IQuoteAppService
    {
        private readonly IQuotationService _quoteService;
        private readonly ILogger<QuoteAppService> _logger;

        public QuoteAppService(IQuotationService quoteService, ILogger<QuoteAppService> logger)
        {
            _quoteService = quoteService;
            _logger = logger;
        }

        public List<Quotation> GetAllQuotes() => _quoteService.GetAll();

        public Quotation GetQuote(int id) => _quoteService.GetById(id);

        public void AddQuote(Quotation quote)
        {
            try
            {
                _logger.LogInformation($"QuoteAppService: Adding new quotation for client: {quote.clientName}");
                _quoteService.Add(quote);
                _logger.LogInformation($"QuoteAppService: Successfully added quotation");
            }
            catch (Exception ex)
            {
                _logger.LogError($"QuoteAppService: Error adding quotation: {ex.Message}");
                _logger.LogError($"QuoteAppService: Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public void UpdateQuote(Quotation quote) => _quoteService.Update(quote);

        public void DeleteQuote(int id) => _quoteService.Delete(id);
    }
}
