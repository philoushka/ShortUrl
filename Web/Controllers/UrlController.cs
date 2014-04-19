using ShortUrl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShortUrl.Controllers
{
    public class UrlController : Controller
    {
        ShortUrlController webApi;
        public UrlController()
        {
            webApi = new ShortUrlController();
        }
        public ActionResult Index()
        {
            ViewBag.ThisUser = HttpContext.User.Identity.Name;
            var userUrls = webApi.GetUserSites(HttpContext.User.Identity.Name);
            ViewBag.PreviousShortUrls = ((IEnumerable<ShortenedUrl>)userUrls).OrderByDescending(x => x.VisitsCount).ThenByDescending(x => x.CreatedOnUTC);
            return View();
        }

        public ActionResult RedirectForToken(string token)
        {
            var shortUrl = webApi.Get(token);
            if (shortUrl != null)
            {
                IncrementVisitCount(shortUrl);
                return new RedirectResult(shortUrl.TargetUrl, false);
            }
            else
            {
                ViewBag.FailedToken = token;
                return View("TokenNotFound");
            }
        }

        private void IncrementVisitCount(ShortenedUrl shortUrl)
        {
            shortUrl.VisitsCount += 1;
            webApi.Update(shortUrl);
        }

        [HttpPost]
        public ActionResult Create(ShortenedUrl shortUrl)
        {

            ModelError shortTokenError = EnsureValidShortToken(shortUrl);
            if (shortTokenError != null)
            {
                ModelState.AddModelError("ShortToken", shortTokenError.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                shortUrl.TargetUrl = EnsureHasHttpScheme(shortUrl.TargetUrl);
                if (IsValidUri(shortUrl.TargetUrl) == false)
                {
                    ModelState.AddModelError("TargetUrl", "That isn't a valid URL");
                    return View(shortUrl);
                }

                shortUrl.CreatedBy = HttpContext.User.Identity.Name;
                shortUrl.CreatedOnUTC = DateTime.UtcNow;
                webApi.CreateNewUrl(shortUrl);

                return RedirectToAction("Index");
            }
            else
            { return View("Index", shortUrl); }
        }

        /// <summary>
        /// Ensure the supplied short token isn't taken and or is URL encoded.        
        /// </summary>
        /// <param name="shortUrl"></param>
        public ModelError EnsureValidShortToken(ShortenedUrl shortUrl)
        {
            if (shortUrl.ShortToken.IsNullOrWhiteSpace())
            {
                do
                {
                    shortUrl.ShortToken = Data.RandomStringGenerator.GenerateRandomString(4);

                } while (ShortTokenIsAvailable(shortUrl.ShortToken) == false);
            }
            else
            {
                if (ShortTokenIsAvailable(shortUrl.ShortToken) == false)
                {
                    return new ModelError("Token [{0}] has already been taken.".FormatWith(shortUrl.ShortToken));
                }
            }
            return null;
        }

        public bool ShortTokenIsAvailable(string proposedToken)
        {
            return (webApi.Get(proposedToken) == null);
        }

        public bool IsValidUri(string url)
        {
            return (Uri.IsWellFormedUriString(url, UriKind.Absolute));
        }

        /// <summary>
        /// Ensure the URI has a scheme that a browser can handle. If not, prefix it with "http://"
        /// </summary>
        /// <param name="uri"></param>
        public string EnsureHasHttpScheme(string uri)
        {
            string[] ValidSchemes = { "http://", "https://", "ftp://", "magnet:?", "sftp://" };
            if (ValidSchemes.Any(x => uri.StartsWith(x)) == false)
            {
                uri = ValidSchemes.First() + uri;
            }
            return uri;
        }
    }

}