using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestMovieSite.Domain.Models;

namespace TestMovieSite.Domain.AppUserManager
{
    public interface IAppUserManager
    {
        Task<IdentityUser> GetUserAsync(ClaimsPrincipal user);

        Task<bool> CheckEditPermissionAsync(ClaimsPrincipal user, Movie movie);
    }
}