using IAB_251_Assessment2.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using IAB_251_Assessment2.DataAccess.Data;
using IAB_251_Assessment2.DataAccess.Interfaces;
using IAB_251_Assessment2.BusinessLogic.Entities;
using System.Collections.Generic;

namespace FleetProLayered_Architecture.DataAccess.Repositories
{
    public class EFQuoteRepository : IQuoteRepository
    {
        private readonly UserManagementDBContext _context;

        public EFQuoteRepository(UserManagementDBContext context)
        {
            _context = context;
        }
        
        public List<Quotation> GetAll() => _context.Quotations.ToList();

        public Quotation GetById(int id) => _context.Quotations.Find(id);

        public void Add(Quotation quote)
        {
            _context.Quotations.Add(quote);
            _context.SaveChanges();
        }

        public void Update(Quotation quote)
        {
            _context.Entry(quote).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var quote = _context.Quotations.Find(id);
            if (quote != null)
            {
                _context.Quotations.Remove(quote);
                _context.SaveChanges();
            }
        }
    }
}
