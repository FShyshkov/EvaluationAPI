using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EvaluationAPI.DAL.Identity.Entities;

namespace EvaluationAPI.DAL.Identity.IdentityContext
{
    public class EvIdentityContext:IdentityDbContext<EvaluationUser>
    {
        public EvIdentityContext(DbContextOptions<EvIdentityContext> options)
              : base(options)
        {
        } 
    }
}
