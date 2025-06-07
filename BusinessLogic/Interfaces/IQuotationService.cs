using IAB_251_Assessment2.BusinessLogic.Entities;

namespace IAB_251_Assessment2.BusinessLogic.Interfaces
{
    public interface IQuotationService
    {

        List<Quotation> GetAll();
        Quotation GetById(int id);
        void Add(Quotation quote);
        void Update(Quotation quote);
        void Delete(int id);
    }
}