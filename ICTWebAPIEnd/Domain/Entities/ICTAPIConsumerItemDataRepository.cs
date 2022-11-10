using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ICTWebAPIEnd.Domain.Entities
{
    public class ICTAPIConsumerItemDataRepository : BaseICTDataRepositoryClass, IAPIBaseDataRepositoryItem
    {
        public ICTAPIConsumerItemDataRepository(IICTDataRepository Repository) : base(Repository)
        {
        }

        public object View<T>(T dataItem, IQueryCollection Params, UserAccountClass user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> ViewAll<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user)
        {
            return repository.Consumer.ViewAll(new List<ParentedContactClass>());
        }
    }
}
