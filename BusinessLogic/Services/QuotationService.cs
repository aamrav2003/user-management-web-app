using IAB_251_Assessment2.BusinessLogic.Entities;
using IAB_251_Assessment2.BusinessLogic.Interfaces;
using IAB_251_Assessment2.DataAccess.Interfaces;

namespace IAB_251_Assessment2.BusinessLogic.Services
{
    public class QuotationService: IQuotationService
    {
        private readonly IQuoteRepository _quoteRepository;

        public QuotationService(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public List<Quotation> GetAll() => _quoteRepository.GetAll();

        public Quotation GetById(int id) => _quoteRepository.GetById(id);

        public void Add(Quotation quote) => _quoteRepository.Add(quote);

        public void Update(Quotation quote) => _quoteRepository.Update(quote);

        public void Delete(int id) => _quoteRepository.Delete(id);
    }
}
