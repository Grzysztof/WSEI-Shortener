using Microsoft.AspNetCore.Mvc;
using Shortener.Web.Models;
using Shortener.Web.Services;

namespace Shortener.Web.Controllers
{
    public class ShortenerController : Controller
    {
        private readonly IShortenerService _shortenerService;

        public ShortenerController(IShortenerService shortenerService)
        {
            _shortenerService = shortenerService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{shortPath}")]
        public IActionResult RedirectRequest(string shortPath)
        {
            var linkItem = _shortenerService.GetLink(shortPath);

            if (linkItem == null)
            {
                return View("Error", new ShortenerErrorModel { Message = "There is no such link in our DB :( " } );
            }

            return Redirect(linkItem.LongUrl);
        }


        [HttpPost]
        public IActionResult Index(UrlModel model)
        {
            var item = _shortenerService.Create(model);
            return View(item);
        }

        [HttpGet("delete/{token}/{shortPath}")]
        public IActionResult Delete(string shortPath, string token)
        {
            _shortenerService.Delete(shortPath, token);
            return View("Delete", new ShortenerDeleteModel { ShortPath = shortPath});
        }

    }
}
