using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortUrl.Models
{
    
    public class ShortenedUrl
    {
        public string TargetUrl { get; set; }
        public string ShortToken { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOnUTC { get; set; }
        public int VisitsCount { get; set; }        
    }
}