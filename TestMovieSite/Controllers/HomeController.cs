using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestMovieSite.Domain.AppUserManager;
using TestMovieSite.Domain.Helpers;
using TestMovieSite.Services;
using TestMovieSite.Views.ViewModels;

namespace TestMovieSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IAppUserManager _userManager;

        public HomeController(IMovieService movieService, IAppUserManager userManager)
        {
            _movieService = movieService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, bool? isSuccess = null)
        {
            var model = await _movieService.GetMoviePage(pageNumber);
            if (isSuccess.HasValue)
            {
                model.Message = OperationResultHelper.GetMessage(isSuccess.Value);
                model.IsSuccessMessage = isSuccess.Value;
            }
            
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MovieViewModel newMovie)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var result = await _movieService.AddOrEditMovie(newMovie.ToModel(), newMovie.NewPoster, currentUser);
            if (result.IsSuccess)
                return RedirectToAction("Details", new {id = result.Data.Id, showSuccessMessage = result.IsSuccess });
            
            return RedirectToAction("Index", new { pageNumber = 1, isSuccess = result.IsSuccess});
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _movieService.GetMovie(id);
            if (!result.IsSuccess)
            {
                return RedirectToAction("Index", new {pageNumber = 1, isSuccess = result.IsSuccess});
            }
            
            var movie = result.Data;
            if (!await _userManager.CheckEditPermissionAsync(User, movie))
            {
                return Forbid();
            }
            
            return View(MovieViewModel.FromModel(movie));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] MovieViewModel movieVm)
        {
            var movieRes = await _movieService.GetMovie(movieVm.Id);
            if (!movieRes.IsSuccess)
            {
                return RedirectToAction("Details", new {id = movieVm.Id, showSuccessMessage = false});
            }
            var movie = movieRes.Data;
            if (!await _userManager.CheckEditPermissionAsync(User, movie))
            {
                return Forbid();
            }
            
            var currentUser = await _userManager.GetUserAsync(User);
            var result = await _movieService.AddOrEditMovie(movieVm.ToModel(), movieVm.NewPoster, currentUser);
            if (result.IsSuccess)
            {
                return RedirectToAction("Details", new {id = result.Data.Id, showSuccessMessage = result.IsSuccess});
            }

            movieVm.Message = OperationResultHelper.GetMessage(result.IsSuccess);
            movieVm.IsSuccessMessage = false;
            return View(movieVm);
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Details(int id, bool showSuccessMessage = false)
        {
            var result = await _movieService.GetMovie(id);
            if (!result.IsSuccess)
            {
                return RedirectToAction("Index", new { pageNumber = 1, isSuccess = result.IsSuccess});
            }
            var movie = result.Data;
            
            var isCurrentUserDownloader = await _userManager.CheckEditPermissionAsync(User, movie);
            var model = MovieViewModel.FromModel(movie, isCurrentUserDownloader);
            if (showSuccessMessage)
            {
                model.Message = OperationResultHelper.GetMessage(isSuccess: true);
                model.IsSuccessMessage = true;
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _movieService.GetMovie(id);
            if (!result.IsSuccess)
            {
                return RedirectToAction("Index", new {pageNumber = 1, isSuccess = false});
            }

            var movie = result.Data;
            if (!await _userManager.CheckEditPermissionAsync(User, movie))
            {
                return Forbid();
            }
            
            return View(MovieViewModel.FromModel(result.Data));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(MovieViewModel movieVm)
        {
            var result = await _movieService.GetMovie(movieVm.Id);
            if (!result.IsSuccess)
            {
                return RedirectToAction("Index", new {pageNumber = 1, isSuccess = false});
            }

            var movie = result.Data;
            if (!await _userManager.CheckEditPermissionAsync(User, movie))
            {
                return Forbid();
            }
            
            var isDeleted= await _movieService.DeleteMovie(movie);
            if(isDeleted)
            {
                return RedirectToAction("Index", new {id = 1, isSuccess = true });
            }

            movieVm.Message = OperationResultHelper.GetMessage(isSuccess: false);
            movieVm.IsSuccessMessage = false;
            return View(movieVm);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
