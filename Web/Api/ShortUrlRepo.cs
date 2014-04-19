using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortUrl.Models;
using Biggy;
using Biggy.JSON;

namespace ShortUrl.Data
{
    public class ShortUrlRepo
    {

        BiggyList<ShortenedUrl> db;
        public ShortUrlRepo()
        {
              db = new BiggyList<ShortenedUrl>(dbPath: HttpRuntime.AppDomainAppPath);

        }
        public ShortenedUrl GetShortenedUrl(string token)
        {
            return db.SingleOrDefault(x => x.ShortToken == token);            
        }

        public void Save(ShortenedUrl shortened)
        {
            db.Add(shortened);
        }

        public void Update(ShortenedUrl shortUrl)
        {
            db.Update(shortUrl);
        }

        public void Remove(ShortenedUrl shortUrl)
        {
            db.Remove(shortUrl);
        }

        public IEnumerable<ShortenedUrl> GetUserSites(string userID)
        {
            return db.Where(x => x.CreatedBy.Equals(userID));
        }
    }
}