using Microsoft.EntityFrameworkCore;

namespace Airpoot.API.DAL
{
    public class HistoryRepository : IRepository<AirplaneHistory>
    {
        private readonly AirportHistoryContext _context;
        public HistoryRepository(AirportHistoryContext context)
        {
            _context = context;
        }

        public void Add(AirplaneHistory entity)
        {
            var a = _context.AirplanesHistory.Add(entity);
            _context.SaveChanges();
        }

        public void Update(AirplaneHistory entity)
        {
            var a = Get(entity.Id);
            if (a == null)
                return;
            else
            {
                a.Code = entity.Code;
                a.IsDeparted = entity.IsDeparted;
                a.Start = entity.Start;
                a.Finish = entity.Finish;
            }
            _context.SaveChanges();
        }
        public AirplaneHistory? Get(Guid Id)
        {
            var a = _context.AirplanesHistory.SingleOrDefault(x => x.Id == Id);
            if (a != null)
                return a;
            else
                return null;
        }

        public IQueryable<AirplaneHistory> GetAll()
        {
            return _context.AirplanesHistory;
        }
        public IQueryable<AirplaneHistory> GetLastFew(int amount)
        {
            return GetAll().OrderByDescending(x => x.Finish).Take(amount);
        }

    }
}
