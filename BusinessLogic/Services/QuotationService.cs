using IAB_251_Assessment2.BusinessLogic.Entities;
using IAB_251_Assessment2.BusinessLogic.Interfaces;
using IAB_251_Assessment2.DataAccess.Interfaces;

namespace IAB_251_Assessment2.BusinessLogic.Services
{
    public class QuotationService: IQuotationService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly ILogger<QuotationService> _logger;

        public QuotationService(IQuoteRepository quoteRepository, ILogger<QuotationService> logger)
        {
            _quoteRepository = quoteRepository;
            _logger = logger;
        }

        public List<Quotation> GetAll() => _quoteRepository.GetAll();

        public Quotation GetById(int id) => _quoteRepository.GetById(id);

        public void Add(Quotation quote)
        {
            try
            {
                _logger.LogInformation($"QuotationService: Adding new quotation for client: {quote.clientName}");
                _quoteRepository.Add(quote);
                _logger.LogInformation($"QuotationService: Successfully added quotation");
            }
            catch (Exception ex)
            {
                _logger.LogError($"QuotationService: Error adding quotation: {ex.Message}");
                _logger.LogError($"QuotationService: Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public void Update(Quotation quote) => _quoteRepository.Update(quote);

        public void Delete(int id) => _quoteRepository.Delete(id);
    }
}
