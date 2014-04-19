using ShortUrl.Data;
using ShortUrl.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace ShortUrl
{
    public class ShortUrlController : ApiController
    {
        ShortUrlRepo repo;
        public ShortUrlController()
        {
            repo = new ShortUrlRepo();
        }
        
        [HttpGet]
        public ShortenedUrl Get(string token)
        {            
            return repo.GetShortenedUrl(token);
        }
      
        [HttpPost]
        public void CreateNewUrl(ShortenedUrl shortUrl)
        {
            repo.Save(shortUrl);
        }

        [HttpPost]
        public void Update(ShortenedUrl shortUrl)
        {
            repo.Update(shortUrl);
        }

        [HttpPost]
        public void Delete(ShortenedUrl shortUrl)
        {
            repo.Remove(shortUrl);
        }

        [HttpGet]
        public IEnumerable<ShortenedUrl> GetUserSites(string userID)
        {
            return repo.GetUserSites(userID);
        }

    }
}