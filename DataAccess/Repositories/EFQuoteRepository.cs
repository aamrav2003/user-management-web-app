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
        private readonly ILogger<EFQuoteRepository> _logger;

        public EFQuoteRepository(UserManagementDBContext context, ILogger<EFQuoteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public List<Quotation> GetAll() => _context.Quotations.ToList();

        public Quotation GetById(int id) => _context.Quotations.Find(id);

        public void Add(Quotation quote)
        {
            try
            {
                _logger.LogInformation($"Adding new quotation for client: {quote.clientName}");
                _context.Quotations.Add(quote);
                _context.SaveChanges();
                _logger.LogInformation($"Successfully added quotation with ID: {quote.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding quotation: {ex.Message}");
                _logger.LogError($"Stack trace: {ex.StackTrace}");
                throw;
            }
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
