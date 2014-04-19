using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortUrl.Models
{
    public class User : IUser
    {        
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }

        
        public DateTime CreatedOnUTC { get; set; }
        public DateTime LastVisitOnUTC { get; set; }
        public IEnumerable<ShortenedUrl> SubmittedUrls { get; set; }
        
        public string SecurityStamp { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        string IUser.Id
        {
            get { return this.UserId.ToString(); }
        }

    }
}