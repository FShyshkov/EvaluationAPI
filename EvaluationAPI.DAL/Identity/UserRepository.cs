using System;
using System.Collections.Generic;
using System.Text;
using EvaluationAPI.DAL.Identity.IdentityContext;
using EvaluationAPI.DAL.Identity.IdentityEntity;
using EvaluationAPI.DAL.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EvaluationAPI.DAL.Identity.Responses;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EvaluationAPI.DAL.Identity
{
    public class UserRepository:IUserRepository
    {
        private readonly UserManager<EvaluationUser> _userManager;
        private readonly EvIdentityContext _context;

        public UserRepository(UserManager<EvaluationUser> userManager, EvIdentityContext userContext)
        {
            _userManager = userManager;
            _context = userContext;
        }

        public async Task<bool> CheckPassword(EvaluationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<CreateUserResponse> Create(string firstName, string lastName, string email, string userName, string password)
        {
            var appUser = new EvaluationUser { Email = email, UserName = userName, FirstName = firstName, LastName = lastName };
            var identityResult = await _userManager.CreateAsync(appUser, password);
            await _userManager.AddToRoleAsync(appUser, "APIUSER");
            if (!identityResult.Succeeded) return new CreateUserResponse(appUser.Id, false, identityResult.Errors.Select(e => new Error(e.Code, e.Description)));
            
            return new CreateUserResponse(appUser.Id, identityResult.Succeeded, identityResult.Succeeded ? null : identityResult.Errors.Select(e => new Error(e.Code, e.Description)));
        }

        public void Update(EvaluationUser entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IList<string>> GetRoles(EvaluationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<EvaluationUser> FindByName(string userName)
        {
            var evUser = await _userManager.FindByNameAsync(userName);
            return evUser;
        }
    }
}
