using ictweb5.Domain.Interfaces;
using ictweb5.Models;

namespace ICTWebAPIEnd.ProxyDataRepository.Entities
{
    public class ICTAPIUserItemDataRepository : APICRUDDataRepositoryItem, IAuthItemDataRepository
    {
        public ICTAPIUserItemDataRepository(IICTDataRepository Repository)
            : base(Repository)
        {
        }

        public bool ChangePwd(UserAccountClass User, string Password)
        {
            return repository.User.ChangePwd(User, Password);
        }

        public bool HasAccess(UserAccountClass User, EntityClass Entity)
        {
            return repository.User.HasAccess(User, Entity);
        }

        public bool Unique(string UserName)
        {
            return repository.User.Unique(UserName);
        }

        public object Validate(string username, string password)
        {
            return repository.User.Validate(username, password);
        }
    }
}
