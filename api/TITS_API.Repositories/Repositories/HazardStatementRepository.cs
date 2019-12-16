using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TITS_API.Architecture;
using TITS_API.Models.Models;
using TITS_API.Repositories.Interfaces;
using System.Linq;

namespace TITS_API.Repositories.Repositories
{
    public class HazardStatementRepository : Repository<HazardStatement, DatabaseContext>
    {
        public HazardStatementRepository(DatabaseContext context) : base(context)
        { }

        public async Task<HazardStatement> GetByCode(string code)
        {
            return await Task.Run(() => _context.HazardStatements.Where(h => h.Code == code).FirstOrDefault());
        }
    }
}
