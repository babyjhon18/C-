using ictweb5.Domain.Interfaces;
using ICTWebAPIEnd.ProxyDataRepository;

namespace ICTWebAPIEnd.Domain.Entities
{
    public class ICTAPIDeviceDataRepository : APICRUDDataRepositoryItem
    {
        public ICTAPIDeviceDataRepository(IICTDataRepository Repository) :
            base(Repository)
        {
        }
    }
}
