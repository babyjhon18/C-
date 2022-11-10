using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ICTWebAPIEnd.AuthorizationPolicyServices
{
    public static class ICTAPIAuthorizationExtension
    {
        public static AuthorizationOptions AddICTAPIPolicies(this AuthorizationOptions options, IConfiguration configuration)
        {
            IICTDataRepository Repository = new SQLICTDataRepository(configuration["ictdb"].ToString(), null);
            var data = Repository.RouteController.ViewAll(new List<RouteControllerClass>());
            options.AddPolicy("UserIsAdmin", policy =>
            {
                policy.RequireClaim("IsAdmin", "True");
            });
            options.AddPolicy("Config.ObjectTree", policy =>
            {
                policy.RequireClaim("Config", "ObjectTree");
            });
            foreach (var controller in data)
            {
                foreach (var action in controller.Actions)
                {
                    if(controller.Area != "")
                    {
                        options.AddPolicy(controller.Area.ToString() + "." + 
                            controller.Name.ToString() + "." + action.Name.ToString(), policy =>
                            {
                                policy.RequireClaim(controller.Area.ToString() + "." + controller.Name.ToString(), action.Name.ToString());
                            });
                    }
                    else
                    {
                        options.AddPolicy(controller.Name.ToString() + "." + action.Name.ToString(), policy =>
                        {
                            policy.RequireClaim(controller.Name.ToString(), action.Name.ToString());
                        });
                    }
                }
            }
            foreach (var report in Repository.ReportEngine.Reports)
            {
                options.AddPolicy("Report." + report.Key.ToString(), policy =>
                {
                    policy.RequireClaim("Report", report.Key.ToString());
                });
            }
            return options;
        }
    }
}
