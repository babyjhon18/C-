using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ICTWebAPIEnd.Domain.Entities
{
    public class ICTAPISystemRoutinesItemDataRepository : APICRUDDataRepositoryItem, IAPISystemRoutines

    {
        public ICTAPISystemRoutinesItemDataRepository(IICTDataRepository Repository)
            : base(Repository)
        {
        }
        public IEnumerable<object> Search<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user)
        {
            return repository.System.Search((SearchType)Convert.ToInt32(Params["searchType"]),
                Params["condition"].ToString(), user);
        }
    }
}
