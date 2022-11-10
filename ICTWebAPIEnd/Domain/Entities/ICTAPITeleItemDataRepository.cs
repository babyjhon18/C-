using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ICTWebAPIEnd.Domain.Entities.Tele;
using ICTWebAPIEnd.ProxyDataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTWebAPIEnd.Domain.Entities
{
    public class ICTAPITeleItemDataRepository : BaseICTDataRepositoryClass, IAPITele
    {
        public IAPITeleObject Object { get; set; }
        public ICTAPITeleItemDataRepository(IICTDataRepository Repository):
            base(Repository)
        {
            Object = new ICTAPITeleObjectItemDataRepository(Repository);
        }
    }
}
