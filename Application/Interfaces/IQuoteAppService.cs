using IAB_251_Assessment2.BusinessLogic.Entities;

namespace IAB_251_Assessment2.Application.Interfaces
{
    public interface IQuoteAppService
    {
        List<Quotation> GetAllQuotes();
        Quotation GetQuote(int id);
        void AddQuote(Quotation quote);
        void UpdateQuote(Quotation quote);
        void DeleteQuote(int id);
    }
}
