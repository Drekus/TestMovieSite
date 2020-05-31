using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestMovieSite.Domain.Models;

namespace TestMovieSite.Domain.AppUserManager
{
    public class AppUserManager: IAppUserManager
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AppUserManager(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<IdentityUser> GetUserAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }
        
        public async Task<bool> CheckEditPermissionAsync(ClaimsPrincipal user, Movie movie)
        {
            var currentUser = await GetUserAsync(user);
            if (currentUser == null)
            {
                return false;
            }
            return currentUser.Id.Equals(movie?.Downloader?.Id);
        }
    }
}