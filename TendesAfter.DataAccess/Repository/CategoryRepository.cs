using TendesAfter.DataAccess.Repository.IRepository;
using TendesAfter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendesAfter.DataAccess.Repository
{
    public class ProducerRepository : Repository<Producer>, IProducerRepository
    {
        private ApplicationDbContext _db;

        public ProducerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Producer obj)
        {
            _db.Producers.Update(obj);
        }
    }
}
