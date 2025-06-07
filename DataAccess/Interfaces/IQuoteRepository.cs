using IAB_251_Assessment2.BusinessLogic.Entities;
namespace IAB_251_Assessment2.DataAccess.Interfaces
{
    public interface IQuoteRepository
    {
        List<Quotation> GetAll();
        Quotation GetById(int id);
        void Add(Quotation user);
        void Update(Quotation user);
        void Delete(int id);
    }
}
