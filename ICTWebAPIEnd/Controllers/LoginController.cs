using ictweb5.Models;
using ICTWebAPI.Models;
using ICTWebAPIEnd.Controllers;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;

namespace ICTWebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class LoginController : CustomICTAPIController
    {
        public LoginController(IAPIDataRepository Repository) :
            base(Repository)
        {
        }

        private const string SECRET_KEY = "indelco favourite";
        public static readonly SymmetricSecurityKey SINGIN_KEY =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(LoginController.SECRET_KEY));

        //example for post:https://localhost:44398/api/Login
        //then body
        /*
          {
              "username":"*******",
              "password":"*******"
          } 
        */
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserCreditnals creditnals)
        {
            UserAccountClass person = ApiRepository.User.Validate(creditnals.UserName, creditnals.Password) as UserAccountClass;
            if (person == null)
            {
                return Forbid();
            }
            if (!person.APIAccessable)
            {
                return Forbid();
            }
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    person.ClientAddress = ip.ToString();
            }
            var identity = GetIdentity(person);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var _claimsKeyValue = new List<KeyValuePair<string, string>>();
            foreach (var _claim in identity.Claims)
            {
                _claimsKeyValue.Add(new KeyValuePair<string, string>(_claim.Type.ToString(), _claim.Value.ToString()));
            }
            var response = new
            {
                access_token = encodedJwt,
                username = creditnals.UserName,
                claims = _claimsKeyValue,
                id = person.ID,
                isAdmin = person.Admin,
            };
            return new JsonResult(response);
        }

        private ClaimsIdentity GetIdentity(UserAccountClass person)
        {
            if (person != null)
            {
                if (person.Admin == true)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin"),
                    };
                    claims.Add(new Claim("UserId", person.ID.ToString()));
                    claims.Add(new Claim("IsAdmin", person.Admin.ToString()));
                    claims.Add(new Claim("ClientAddress", person.ClientAddress));
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    this.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
                    return claimsIdentity;
                }
                else
                {
                    var claims = new List<Claim>();
                    foreach (var reportClaim in person.Reports)
                    {
                        claims.Add(new Claim("Report", reportClaim.Key.ToString()));
                    }
                    foreach (var claim in person.AccessRoutes)
                    {
                        string[] splitClaim = claim.Split('.');
                        if (splitClaim.Length == 2)
                            claims.Add(new Claim(splitClaim[0].ToString(), splitClaim[1].ToString()));
                        else
                            claims.Add(new Claim(splitClaim[0].ToString() + "." + splitClaim[1].ToString(),
                                splitClaim[2].ToString()));
                    }
                    claims.Add(new Claim("Username", person.Name));
                    claims.Add(new Claim("UserId", person.ID.ToString()));
                    claims.Add(new Claim("IsAdmin", person.Admin.ToString()));
                    claims.Add(new Claim("ClientAddress", person.ClientAddress));
                    claims.Add(new Claim("Config", "ObjectTree"));
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    this.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
                    return claimsIdentity;
                }
            }
            return null;
        }
    }
}
