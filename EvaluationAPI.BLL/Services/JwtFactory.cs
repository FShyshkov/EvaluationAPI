using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using EvaluationAPI.BLL.DTO;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.BLL.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;

namespace EvaluationAPI.BLL.Services
{
    internal sealed class JwtFactory : IJwtFactory
    {
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IJwtTokenHandler jwtTokenHandler, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<AccessToken> GenerateEncodedToken(string id, string userName, List<string> roles)
        {
            var identity = GenerateClaimsIdentity(id, userName, roles);

            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst(EvaluationAPI.BLL.Constants.Constants.Strings.JwtClaimIdentifiers.Rol),
                 identity.FindFirst(EvaluationAPI.BLL.Constants.Constants.Strings.JwtClaimIdentifiers.Id)
             };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                _jwtOptions.NotBefore,
                _jwtOptions.Expiration,
                _jwtOptions.SigningCredentials);

            return new AccessToken(_jwtTokenHandler.WriteToken(jwt), (int)_jwtOptions.ValidFor.TotalSeconds);
        }

        private static ClaimsIdentity GenerateClaimsIdentity(string id, string userName, List<string> roles)
        {
            var claims = new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(EvaluationAPI.BLL.Constants.Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(EvaluationAPI.BLL.Constants.Constants.Strings.JwtClaimIdentifiers.Rol, EvaluationAPI.BLL.Constants.Constants.Strings.JwtClaims.ApiAccess)
            });            
            //extra claims
            //foreach (var role in roles)
            //{
            //    if (role == "MODERATOR")
            //    {
            //        claims.AddClaim(new Claim(EvaluationAPI.BLL.Constants.Constants.Strings.JwtClaimIdentifiers.TestA, EvaluationAPI.BLL.Constants.Constants.Strings.JwtClaims.TestAccess));
            //    }
            //    if (role == "ADMIN")
            //    {
            //        claims.AddClaim(new Claim(EvaluationAPI.BLL.Constants.Constants.Strings.JwtClaimIdentifiers.TestA, EvaluationAPI.BLL.Constants.Constants.Strings.JwtClaims.TestAccess));
            //        claims.AddClaim(new Claim(EvaluationAPI.BLL.Constants.Constants.Strings.JwtClaimIdentifiers.UserA, EvaluationAPI.BLL.Constants.Constants.Strings.JwtClaims.UserAccess));
            //    }
            //}
            return claims;
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
