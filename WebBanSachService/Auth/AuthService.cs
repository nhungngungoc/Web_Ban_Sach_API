using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebBanSachRepository.UserRepo;
using Common.Helper;

namespace WebBanSachService.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _repo;
        private readonly JwtSettings _jwtSetting;
        public AuthService(IUserRepo repo, IOptions<JwtSettings> jwtSettings)
        {
            _repo = repo;
            this._jwtSetting = jwtSettings.Value;
        }
        public dynamic checklogin(string username, string password)
        {
            try
            {
                var user = this._repo.FindOne(x => x.Email.Equals(username) && x.DeleteAt == null);
                if (user == null || !Helper.verifyPassword(password, user.MatKhau))
                {
                    return string.Empty;
                }
                return new
                {
                    accessToken = new
                    {
                        token = this.GenerateToken(user, JwtConstant.expiresIn),
                        expiresIn = JwtConstant.refresh_expiresIn
                    },
                    refreshToken = new
                    {
                        token = this.GenerateToken(user, JwtConstant.refresh_expiresIn),
                        expiresIn = JwtConstant.refresh_expiresIn
                    },
                    Role= user.Quyen
                };
            }
            catch (Exception ex)
            {
                throw new CommonException("Erro check login Authservice", 500, ex);
            }
        }
        private string GenerateToken(WebBanSachModel.Entity.User user, int time)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtSetting.Secret));

            var signingCredential = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimsConstant.ROLE,user.Quyen),
                //new Claim(ClaimTypes.Name,user.HoVaTen),
                new Claim(ClaimsConstant.USER_ID,user.Id.ToString())
            };

            var token = new JwtSecurityToken
            (
                  issuer: this._jwtSetting.Issuer,
                  audience: this._jwtSetting.Audience,
                  expires: DateTime.Now.AddSeconds(time),
                  notBefore: DateTime.Now,
                  signingCredentials: signingCredential,
                  claims: claims
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
