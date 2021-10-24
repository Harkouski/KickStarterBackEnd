using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Token
{
    public class TokenLogic : Controller
    {
        SampleContext db;

        public TokenLogic(SampleContext context)
        {
            db = context;
        }

        public object GetToken(string username)
        {
            var identity = GetIdentity(username);
            User user = db.Users.FirstOrDefault(x => x.Email == username);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                id = identity.Claims.ToArray()[1].Value
               
            };

            return Json(response);
        }

        private ClaimsIdentity GetIdentity(string email)
        {
            User user = db.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                     new Claim(ClaimsIdentity.DefaultIssuer, user.Email),
                    new Claim(ClaimsIdentity.DefaultIssuer, user.Id),
                   
                };
                ClaimsIdentity claimsIdentity =
                
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultIssuer,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}
