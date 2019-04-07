using Microsoft.AspNetCore.Identity;

namespace EvaluationAPI.DAL.Identity.Entities
{
    public class EvaluationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
