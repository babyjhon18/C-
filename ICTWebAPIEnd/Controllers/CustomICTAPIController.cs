using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace ICTWebAPIEnd.Controllers
{
    public class CustomICTAPIController : ControllerBase
    {
        public CustomICTAPIController(IAPIDataRepository APIRepository)
        {
            this.ApiRepository = APIRepository;
        }

        public IAPIDataRepository ApiRepository;

        public UserAccountClass CurrentUser
        {
            get
            {
                return new UserAccountClass(Convert.ToBoolean(HttpContext.User.Claims.
                    Where(c => c.Type == "IsAdmin").Select(c => c.Value).SingleOrDefault().ToString()))
                {
                    ID = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "UserId").
                    Select(c => c.Value).SingleOrDefault().ToString()),
                    ClientAddress = HttpContext.User.Claims.Where(c => c.Type == "ClientAddress").
                    Select(c => c.Value).SingleOrDefault().ToString()
                };
            }
        }

        public object Status(dynamic dataItem)
        {
            if (dataItem is Boolean)
            {
                if (Convert.ToBoolean(dataItem))
                    Response.StatusCode = 200;
                else
                    Response.StatusCode = 304;
            }
            else
            {
                if (dataItem == null)
                {
                    Response.StatusCode = 400;
                    return new EmptyResult();
                }
            }
            return dataItem;
        }

        [HttpPost]
        public virtual void Post([FromBody] Object dataItem, int entityID = 0)
        {
            var entity = JsonConvert.DeserializeObject<EntityClass>(dataItem.ToString());
            if (entity.Name != "")
            {
                if (entityID != 0 && entity.ID != 0)
                    Status(DoUpdate(dataItem, entityID));
                else if (entityID == 0 && entity.ID == -1)
                    Status(DoCreate(dataItem));
                else
                    Status(false);
            }
            else
            {
                Status(false);
            }
        }

        [HttpDelete]
        public virtual void Delete(int entityID)
        {
            if (entityID != 0)
                Status(DoDelete(entityID));
            else
                Status(false);
        }

        protected virtual bool DoCreate(Object dataItem)
        {
            return default;
        }

        protected virtual bool DoUpdate(Object dataItem, int entityID)
        {
            return default;
        }

        protected virtual bool DoDelete(int entityID)
        {
            return default;
        }
    }
}
