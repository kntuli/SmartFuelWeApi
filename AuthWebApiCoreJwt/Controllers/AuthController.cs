using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthWebApiCoreJwt.DataProvider;
using AuthWebApiCoreJwt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AuthWebApiCoreJwt.Controllers
{
    [Authorize]
    [Produces("application/json")]
    //[Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserDataProvider userDataProvider;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthController(IUserDataProvider userDataProvider, IConfiguration configuration, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            this.userDataProvider = userDataProvider;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Route("users")]
        public async Task<IEnumerable<Users>> Get2()
        {
          return await userDataProvider.GetUsers();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Signup([FromBody]Users user)
        {
            Users x = userDataProvider.GetUserByUsername(user.Email).Result;
            //var user = _usersDb.Users.SingleOrDefault(u => u.Username == username);
            if (x != null) return StatusCode(409);

            user.Password = _passwordHasher.GenerateIdentityV3Hash(user.Password);

            await this.userDataProvider.AddUser(user);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Login([FromBody]LoginModel user)
        {
            Users x = userDataProvider.GetUserByUsername(user.UserName).Result;
            if (x == null || !_passwordHasher.VerifyIdentityV3Hash(user.Password, x.Password)) return BadRequest(new { success = false, message = "Username or password is incorrect", status = 400 });

            var usersClaims = new[]
            {
                new Claim(ClaimTypes.Name, x.Username),
                new Claim(ClaimTypes.NameIdentifier, x.Id.ToString()),
                new Claim("UserID", x.Id.ToString()),
                new Claim("FirstName", x.FirstName),
                new Claim("LastName", x.LastName),
                new Claim("Mobile", x.Mobile),
                new Claim("Email", x.Email),
                new Claim("RoleID", x.RoleId.ToString())
            };

            var jwtToken = _tokenService.GenerateAccessToken(usersClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            x.RefreshToken = refreshToken;
            await userDataProvider.UpdateUserRefreshToken(x);//Save Refresh Token

            return Ok(new
            {
                success = true,
                token = jwtToken,
                refreshToken = refreshToken
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> Refresh(string token, string refreshToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            Users user = userDataProvider.GetUserByUsername(username).Result;
            //var user = _usersDb.Users.SingleOrDefault(u => u.Username == username);
            if (user == null || user.RefreshToken != refreshToken) return BadRequest();

            var newJwtToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await userDataProvider.UpdateUserRefreshToken(user);//Save Refresh Token

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpPost]
        [Route("removetoken")]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity.Name;

            Users user = userDataProvider.GetUserByUsername(username).Result;
            if (user == null) return BadRequest();

            user.RefreshToken = null;

            await userDataProvider.UpdateUserRefreshToken(user);//Detele old Refresh Token

            return NoContent();
        }



        ////login
        //[AllowAnonymous]
        //[HttpPost, Route("login")]
        //public IActionResult Login([FromBody]LoginModel user)
        //{

        //    Users x = amenDataProvider.GetUserByUsername(user.UserName).Result;
        //    if (x == null)
        //        return BadRequest(new { message = "Username or password is incorrect" }); 

        //    if (user.Password == x.Password)
        //    {
        //        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));
        //        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //        //var tokeOptions = new JwtSecurityToken(
        //        //    issuer: "http://localhost:5000",
        //        //    audience: "http://localhost:5000",
        //        //    claims: new List<Claim>(),
        //        //    expires: DateTime.Now.AddMonths(12),
        //        //    signingCredentials: signinCredentials
        //        //);

        //        var tokeOptions = new JwtSecurityToken(
        //            issuer: _configuration["Jwt:Site"],
        //            audience: _configuration["Jwt:Site"],
        //            claims: new List<Claim>(),
        //            expires: DateTime.Now.AddMonths(Convert.ToInt32(_configuration["Jwt:ExpiryInHours"])),
        //            signingCredentials: signinCredentials
        //        );

        //        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        //        return Ok(new { success = true, Token = tokenString });
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }

        //}

        //[AllowAnonymous]
        //[HttpPost, Route("login2")]
        //public async Task<ActionResult> Login2([FromBody]LoginModel user)
        //{
        //    Users x = await amenDataProvider.GetUserByUsername(user.UserName);
        //    if (x == null)
        //        return BadRequest(new { message = "Username or password is incorrect" });

        //    if (user.Password == x.Password)
        //    {
        //        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));
        //        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //        //await _loginUseCase.Handle(new LoginRequest(request.UserName, request.Password, Request.HttpContext.Connection.RemoteIpAddress?.ToString()), _loginPresenter);
        //        var tokeOptions = new JwtSecurityToken(
        //            issuer: _configuration["Jwt:Site"],
        //            audience: _configuration["Jwt:Site"],
        //            claims: new List<Claim>(),
        //            expires: DateTime.Now.AddMonths(Convert.ToInt32(_configuration["Jwt:ExpiryInMonths"])),
        //            signingCredentials: signinCredentials
        //        );

        //        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        //        return Ok(new { success = true, Token = tokenString });
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}

        //[AllowAnonymous]
        //[HttpPost, Route("refreshtoken")]
        //public async Task<ActionResult> refreshtoken([FromBody]LoginModel user)
        //{
        //    var cp = _jwtTokenValidator.GetPrincipalFromToken(user.AccessToken, user.SigningKey);

        //    // invalid token/signing key was passed and we can't extract user claims
        //    if (cp != null)
        //    {
        //        var id = cp.Claims.First(c => c.Type == "id");
        //        var user = await _userRepository.GetSingleBySpec(new UserSpecification(id.Value));

        //        if (user.HasValidRefreshToken(message.RefreshToken))
        //        {
        //            var jwtToken = await _jwtFactory.GenerateEncodedToken(user.IdentityId, user.UserName);
        //            var refreshToken = _tokenFactory.GenerateToken();
        //            user.RemoveRefreshToken(message.RefreshToken); // delete the token we've exchanged
        //            user.AddRefreshToken(refreshToken, user.Id, ""); // add the new one
        //            await _userRepository.Update(user);
        //            outputPort.Handle(new ExchangeRefreshTokenResponse(jwtToken, refreshToken, true));
        //            return true;
        //        }
        //    }
        //    outputPort.Handle(new ExchangeRefreshTokenResponse(false, "Invalid token."));
        //    return false;
        //}

        // [AllowAnonymous]
        //[HttpGet]
        //[Route("roles")]
        //public async Task<IEnumerable<UserRoles>> roles()
        //{
        //    return await userDataProvider.GetUserRoles();
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("AddUser")]
        //public async Task Post([FromBody]Users user)
        //{
        //    await this.userDataProvider.AddUser(user);
        //}

        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task Put(int id, [FromBody]Users user)
        {
            await this.userDataProvider.UpdateUser(user);
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task Delete(string id)
        {
            await this.userDataProvider.DeleteUser(id);
        }

        //[HttpPost]
        //[Route("login0")]
        //public async Task<IActionResult> GetAsync([FromBody]LoginModel user)
        //{
        //    Users xUser = await userDataProvider.GetUserByUsername(user.UserName);

        //    var userId = xUser.Id;

        //    UserRoles xUserRole = await userDataProvider.GetUserRole(userId);

        //    if (user == null)
        //    {
        //        return BadRequest("Invalid client request");
        //    }

        //    //Users x = userDataProvider.GetUserByUsername(user.UserName).Result;

        //    if (user.Password == xUser.Password)
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, user.UserName),
        //            new Claim(ClaimTypes.Role, xUserRole.RoleName),
        //            new Claim(ClaimTypes.Email, user.UserName),
        //            new Claim("Company", "BIS247 Pty Ltd")
        //        };

        //        //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));


        //        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));
        //        var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

        //        int expiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

        //        var tokeOptions = new JwtSecurityToken(
        //            issuer: _configuration["Jwt:Site"],
        //            audience: _configuration["Jwt:Site"],
        //            claims: claims,
        //            expires: DateTime.Now.AddMinutes(expiryInMinutes),
        //            signingCredentials: signinCredentials
        //        );

        //        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        //        return Ok(new { Token = tokenString });
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}






    }


}