using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Omu.ValueInjecter;
using Tribes.Database.Context;
using Tribes.Database.Context.Entities;
using Tribes.Database.Dal.Poco;

namespace Tribes.Database.Dal
{
    public class TribesService
    {
        private readonly IDbContext _context = new TribesContext();
        private readonly UnitOfWork _unitOfWork;
        private readonly IValueInjecter _injecter = new ValueInjecter();

        public TribesService()
        {
            _unitOfWork = new UnitOfWork(_context);
            _injecter = new ValueInjecter();
        }

        internal TribesService(IDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }


        public List<TribePoco> GetAllTribes()
        {
            var tribes = _unitOfWork.Repository<Tribe>().Get();

            var pocoTribes = new List<TribePoco>();
            
            if (tribes.Any()) 
                tribes.ForEach(tribe => pocoTribes.Add(GetPoco<TribePoco, Tribe>(tribe)));

            return pocoTribes;
        }

        private TTargetType GetPoco<TTargetType, TSourceType>(TSourceType tribe) where TTargetType : new()
        {
            var poco = new TTargetType();
            _injecter.Inject(poco, tribe);
            return poco;
        }
    }
}
