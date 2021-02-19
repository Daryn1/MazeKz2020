using Housing.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Housing
{
    static class AuthentificationOptions
    {
        internal const string AUDIENCE = "Audience";
        internal const string ISSUER = "Issuer";
        internal const int LIFETIME = 1;

        public static ClaimsIdentity CreateIdentity(HousingOwner user)
        {
            if (user == null) return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.User.Login),
                new Claim(ClaimTypes.Role, "HousingOwner"),
            };
           var identity =  new ClaimsIdentity(claims, "CookieAuthentification",
               ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return identity;
        }
    }
}
