using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortener.Web.Entities
{
    public class UrlEntity
    {
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public DateTime CreationDate { get; set; }
        public String validationString { get; set; }
    }
}
